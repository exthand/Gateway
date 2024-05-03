
using Exthand.GatewayClient.Interface;
using Exthand.GatewayClient.Models;

using Newtonsoft.Json;

using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using Exthand.GatewayClient.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace Exthand.GatewayClient
{
    public class GatewayService : IGatewayService
    {
        private IHttpClientFactory _httpClientFactory;

        public GatewayService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region UTILITIES

        /// <summary>
        /// Returns a list of activated banks for a given country.
        /// </summary>
        /// <param name="countryCode">ISO-2 of the country ("BE", "FR, etc)</param>
        /// <returns>A list of Bank objects.</returns>
        public async Task<IEnumerable<Bank>> GetBanksAsync(string countryCode="")
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            var result = await client.GetAsync("ob/banks?countryCode=" + countryCode);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Bank>>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }

            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }


        public async Task<string> FindFlowIdAsync(string queryString)
        {


            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            var result = await client.GetAsync("ob/findFlowId?queryString=" + HttpUtility.UrlEncode(queryString));

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        #endregion

        #region PIS

        public async Task<BankPaymentAccessOption> GetBankPaymentAccessOptionsAsync(int connectorId)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            var result = await client.GetAsync("ob/pis/payments/options/" + connectorId.ToString());

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BankPaymentAccessOption>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<PaymentInitResponse> PaymentInitiateAsync(PaymentInitRequest paymentInitRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(paymentInitRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/payments", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PaymentInitResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<PaymentFinalizeResponse> PaymentFinalizeAsync(PaymentFinalizeRequest paymentFinalizeRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(paymentFinalizeRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/pis/payments", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PaymentFinalizeResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<PaymentStatusResponse> PaymentStatusAsync(PaymentStatusRequest paymentStatusRequest)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(paymentStatusRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/payments/status", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PaymentStatusResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<BulkPaymentInitResponse> BulkPaymentInitiateAsync(BulkPaymentInitRequest bulkPaymentInitRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(bulkPaymentInitRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/payments/bulk", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BulkPaymentInitResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<BulkPaymentFinalizeResponse> BulkPaymentFinalizeAsync(BulkPaymentFinalizeRequest bulkPaymentFinalizeRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(bulkPaymentFinalizeRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/pis/payments/bulk", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BulkPaymentFinalizeResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<BulkPaymentStatusResponse> BulkPaymentStatusAsync(BulkPaymentStatusRequest bulkPaymentStatusRequest)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(bulkPaymentStatusRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/payments/bulk/status", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BulkPaymentStatusResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Initiates the process of getting a consent for bank accounts. 
        /// </summary>
        /// <param name="bankAccessRequest"></param>
        /// <returns></returns>
        public async Task<PaymentFinalizeResponse> GetAccountsForPaymentsInitAsync(AccountsForPaymentRequestInit accountsForPaymentRequestInit)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(accountsForPaymentRequestInit), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/accounts", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PaymentFinalizeResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Finalizes the request of consent. Must be called after the redirect from the bank.
        /// </summary>
        /// <param name="bankAccessRequestFinalize"></param>
        /// <returns></returns>
        public async Task<AccountsForPaymentFinalize> GetAccountsForPaymentsFinalizeAsync(AccountsForPaymentFinalizeRequest accountsForPaymentFinalizeRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(accountsForPaymentFinalizeRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/pis/accounts", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<AccountsForPaymentFinalize>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        #endregion

        #region AIS-BANK-ACCESS

        /// <summary>
        /// Returns the Bank access options for a given connector.
        /// </summary>
        /// <param name="connectorId"></param>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<BankAccessOption> GetBankAccessOptionsAsync(int connectorId)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            var result = await client.GetAsync("ob/ais/access/options/" + connectorId.ToString());

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BankAccessOption>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Initiates the process of getting a consent for bank accounts. 
        /// </summary>
        /// <param name="bankAccessRequest"></param>
        /// <returns></returns>
        public async Task<BankAccessResponse> RequestBankAccessAsync(BankAccessRequest bankAccessRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(bankAccessRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/ais/access", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BankAccessResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Finalizes the request of consent. Must be called after the redirect from the bank.
        /// </summary>
        /// <param name="bankAccessRequestFinalize"></param>
        /// <returns></returns>
        public async Task<BankAccessResponseFinalize> FinalizeRequestBankAccessAsync(BankAccessRequestFinalize bankAccessRequestFinalize)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(bankAccessRequestFinalize), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/ais/access", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BankAccessResponseFinalize>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Cancels a token for a given PSU and Connector.
        /// </summary>
        /// <param name="DeleteConsentRequest"></param>
        /// <returns>DeleteRequestResponse</returns>
        public async Task<bool> CancelBankAccessAsync(DeleteConsentRequest deleteConsentRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(deleteConsentRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/ais/consents/delete", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Removes a bank account from the list of available accounts for a given user.
        /// </summary>
        /// <param name="DeleteAccountRequest"></param>
        /// <returns>DeleteAccountResponse</returns>
        public async Task<bool> RemoveBankAccountAsync(DeleteAccountRequest deleteAccountRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(deleteAccountRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/ais/accounts/delete", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<BankAccountsResponse> GetBankAccountsAsync(BankAccountsRequest bankAccountsRequest)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(bankAccountsRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/ais/accounts", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BankAccountsResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        #endregion

        #region AIS-TRANSACTIONS

        public async Task<BalanceResponse> GetBalancesAsync(string accountId, BalanceRequest balanceRequest)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(balanceRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync($"ob/ais/accounts/{accountId}/balances", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BalanceResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<TransactionResponse> GetTransactionsAsync(string accountId, TransactionRequest transactionRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(transactionRequest), Encoding.UTF8, "application/json");

            HttpResponseMessage result = await client.PostAsync($"ob/ais/accounts/{accountId}/transactions", stringContent);

            if (result.IsSuccessStatusCode)
            {
                TransactionResponse transactionResponse = JsonConvert.DeserializeObject<TransactionResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                if (result.StatusCode == HttpStatusCode.PartialContent)
                    transactionResponse.isConsentLost = true;
                return transactionResponse;
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<TransactionResponse> GetTransactionsNextAsync(string accountId, TransactionPagingRequest transactionRequest)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");

            var stringContent = new StringContent(JsonConvert.SerializeObject(transactionRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync($"ob/ais/accounts/{accountId}/transactions/next", stringContent);

            if (result.IsSuccessStatusCode)
            {
                TransactionResponse transactionResponse= JsonConvert.DeserializeObject<TransactionResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                if (result.StatusCode == HttpStatusCode.PartialContent)
                    transactionResponse.isConsentLost = true;
                return transactionResponse;
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        #endregion

        #region USER

        /// <summary>
        /// Gets the content of the latest Terms & Conditions, Pricvacy Notice and their Version number.
        /// </summary>
        /// <returns>TermsDTO object</returns>
        public async Task<TermsDTO> GetTCAsync(string language="")
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            var result = await client.GetAsync("ob/gw/tc/latest?language=" + language);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TermsDTO>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }

            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Get the latest version number of the TC & Privacy accepted by the given user.
        /// </summary>
        /// <param name="psuId">Your internal id of the user (PSU)</param>
        /// <returns>TermsValidatedDTO object</returns>
        public async Task<TermsValidated> GetTCLatestAsync(string psuId)
        {
            TermsValidated termsValidatedDTO = new()
            {
                psuId = psuId,
                version = -1
            };

            if (string.IsNullOrEmpty(psuId))
                return termsValidatedDTO;

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            var result = await client.GetAsync($"ob/gw/users/{psuId}/tc/latest" );

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TermsValidated>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }

            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Creates a new user on the Gateway
        /// </summary>
        /// <param name="userDTO">A userDTO object containes info needed for Extahd:Gateway being able to manage your user (PSU)</param>
        /// <returns>ResponseActionDTO object</returns>
        public async Task<UserRegisterResponse> CreateUserAsync(UserDTO userDTO)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            var stringContent = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("ob/gw/users", stringContent);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserRegisterResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }


        #endregion


        #region ErrorMgmt

        /// <summary>
        /// Generates the Error object being handled and sent back.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task<Error> GetGWError(HttpResponseMessage result)
        {
            var error = JsonConvert.DeserializeObject<Error>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            if (result.Headers.TryGetValues("X-Correlation-ID", out var valuesCo))
                error.xCorrelationId = string.Join(",", valuesCo);

            if (result.Headers.TryGetValues("X-Operation-ID", out var valuesOp))
                error.xOperationId = string.Join(",", valuesOp);

            return error;
        }

        #endregion

    }
}
