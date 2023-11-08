using Exthand.GatewayClient.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Exthand.GatewayClient
{
    public static class GatewayConfigurator
    {
        public static void AddGatewayService(this IServiceCollection services, Uri serverUrl, int httpClientTimeoutMilliseconds = 300000)
        {
            services.AddHttpClient("BankingSdkGatewayClient", c =>
            {
                c.BaseAddress = serverUrl;
                c.Timeout = TimeSpan.FromMilliseconds(httpClientTimeoutMilliseconds);

            });
            services.AddScoped<IGatewayService, GatewayService>();
        }
    }
}
