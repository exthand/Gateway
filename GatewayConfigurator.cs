using Exthand.GatewayClient.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using System;
using System.Net.Http;

namespace Exthand.GatewayClient
{
    /// <summary>
    /// Tunables for the resilience pipelines applied to the gateway HTTP clients.
    /// Defaults are sized for Open Banking calls — which are slow (AIS transaction pulls can take
    /// minutes) and rate-limited by the banks — NOT for fast micro-service calls. They intentionally
    /// differ from <c>AddStandardResilienceHandler()</c>'s defaults (30 s total / 10 s per attempt),
    /// which would cut off legitimate slow bank calls.
    /// </summary>
    public class GatewayResilienceOptions
    {
        /// <summary>Timeout for a single HTTP attempt. Bank calls can be slow, so this is generous.</summary>
        public TimeSpan AttemptTimeout { get; set; } = TimeSpan.FromSeconds(120);

        /// <summary>Overall timeout for the whole pipeline (all attempts). Must be &gt;= <see cref="AttemptTimeout"/>.</summary>
        public TimeSpan TotalRequestTimeout { get; set; } = TimeSpan.FromSeconds(300);

        /// <summary>
        /// Retry attempts for IDEMPOTENT operations only (reads, status, deletes). Payment/consent/user
        /// writes are NEVER retried, regardless of this value.
        /// </summary>
        public int MaxRetryAttempts { get; set; } = 2;

        /// <summary>Base delay for the (jittered, exponential) retry backoff.</summary>
        public TimeSpan RetryBaseDelay { get; set; } = TimeSpan.FromSeconds(1);

        /// <summary>Circuit-breaker failure ratio (0..1) over the sampling window that opens the breaker.</summary>
        public double CircuitBreakerFailureRatio { get; set; } = 0.5;

        /// <summary>Minimum calls in the sampling window before the breaker can open. Low, to suit a low-volume gateway.</summary>
        public int CircuitBreakerMinimumThroughput { get; set; } = 10;

        /// <summary>Sampling window for the circuit breaker.</summary>
        public TimeSpan CircuitBreakerSamplingDuration { get; set; } = TimeSpan.FromSeconds(240);

        /// <summary>How long the breaker stays open before probing again.</summary>
        public TimeSpan CircuitBreakerBreakDuration { get; set; } = TimeSpan.FromSeconds(15);
    }

    public static class GatewayConfigurator
    {
        /// <summary>
        /// Named client for IDEMPOTENT operations (reads, status, deletes):
        /// timeout -&gt; retry -&gt; circuit breaker -&gt; per-attempt timeout.
        /// </summary>
        public const string ClientName = "BankingSdkGatewayClient";

        /// <summary>
        /// Named client for NON-IDEMPOTENT operations (payment/consent init &amp; finalize, user creation):
        /// timeout -&gt; circuit breaker -&gt; per-attempt timeout, with NO retry — a timed-out write may already
        /// have been processed by the bank, so retrying could duplicate it (e.g. a double payment).
        /// </summary>
        public const string ClientNameNoRetry = "BankingSdkGatewayClientNoRetry";

        /// <summary>
        /// Backwards-compatible overload. <paramref name="httpClientTimeoutMilliseconds"/> becomes the total
        /// request timeout; the per-attempt timeout is capped at the same value (max 120 s).
        /// </summary>
        public static void AddGatewayService(this IServiceCollection services, Uri serverUrl, int httpClientTimeoutMilliseconds = 300000)
        {
            var total = TimeSpan.FromMilliseconds(httpClientTimeoutMilliseconds);
            var options = new GatewayResilienceOptions
            {
                TotalRequestTimeout = total,
                AttemptTimeout = total < TimeSpan.FromSeconds(120) ? total : TimeSpan.FromSeconds(120)
            };
            services.AddGatewayService(serverUrl, options);
        }

        /// <summary>
        /// Registers the gateway client with two resilience profiles: a retrying one for idempotent
        /// operations and a non-retrying one for payment/consent/user writes.
        /// </summary>
        public static void AddGatewayService(this IServiceCollection services, Uri serverUrl, GatewayResilienceOptions options)
        {
            if (options is null)
                options = new GatewayResilienceOptions();

            // Idempotent operations: timeout -> retry -> circuit breaker -> per-attempt timeout.
            services.AddHttpClient(ClientName, c =>
            {
                c.BaseAddress = serverUrl;
                c.Timeout = System.Threading.Timeout.InfiniteTimeSpan; // the resilience pipeline owns timeouts
            }).AddResilienceHandler("gw-idempotent", builder =>
            {
                builder
                    .AddTimeout(options.TotalRequestTimeout)
                    .AddRetry(new HttpRetryStrategyOptions
                    {
                        MaxRetryAttempts = options.MaxRetryAttempts,
                        BackoffType = DelayBackoffType.Exponential,
                        UseJitter = true,
                        Delay = options.RetryBaseDelay
                    })
                    .AddCircuitBreaker(BuildBreaker(options))
                    .AddTimeout(options.AttemptTimeout);
            });

            // Non-idempotent operations: NO retry (only timeout + circuit breaker).
            services.AddHttpClient(ClientNameNoRetry, c =>
            {
                c.BaseAddress = serverUrl;
                c.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
            }).AddResilienceHandler("gw-write", builder =>
            {
                builder
                    .AddTimeout(options.TotalRequestTimeout)
                    .AddCircuitBreaker(BuildBreaker(options))
                    .AddTimeout(options.AttemptTimeout);
            });

            services.AddScoped<IGatewayService, GatewayService>();
        }

        private static HttpCircuitBreakerStrategyOptions BuildBreaker(GatewayResilienceOptions options)
            => new HttpCircuitBreakerStrategyOptions
            {
                FailureRatio = options.CircuitBreakerFailureRatio,
                MinimumThroughput = options.CircuitBreakerMinimumThroughput,
                SamplingDuration = options.CircuitBreakerSamplingDuration,
                BreakDuration = options.CircuitBreakerBreakDuration
            };
    }
}
