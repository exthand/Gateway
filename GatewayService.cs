
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
using System.Linq;
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


        /// <summary>
        /// Sets the XRequestID value if provided.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="headerContent"></param>
        /// <returns></returns>
        private HttpClient SetHeaders(HttpClient httpClient, string? headerContent = null)
        {
            if (headerContent is not null)
            {
                httpClient.DefaultRequestHeaders.Add("X-Request-ID", headerContent);
            }
            return httpClient;
        }

        /// <summary>
        /// Sends back a BASE object with XRequestID, XCorrelationID and XOperationID values from answer's Headers set.
        /// </summary>
        /// <param name="baseClass"></param>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        private IBase GetHeaders(IBase baseClass, HttpResponseMessage responseMessage)
        {
            if (responseMessage.Headers.Contains(("X-Request-ID")))
            {
                baseClass.XRequestID = responseMessage.Headers.GetValues("X-Request-ID").First();
            }
            if (responseMessage.Headers.Contains(("X-Correlation-ID")))
            {
                baseClass.XCorrelationID = responseMessage.Headers.GetValues("X-Correlation-ID").First();
            }
            if (responseMessage.Headers.Contains(("X-Operation-ID")))
            {
                baseClass.XOperationID = responseMessage.Headers.GetValues("X-Operation-ID").First();
            }
            return baseClass;
        }
        
        
        #region UTILITIES

        /// <summary>
        /// Returns a list of activated banks for a given country.
        /// </summary>
        /// <param name="countryCode">ISO-2 of the country ("BE", "FR, etc)</param>
        /// <returns>A list of Bank objects.</returns>
        public async Task<BankList> GetBanksAsync(string countryCode="", string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            var result = await client.GetAsync("ob/banks?countryCode=" + countryCode);

            if (result.IsSuccessStatusCode)
            {
                BankList bankList = new();
                bankList.Banks = JsonConvert.DeserializeObject<IEnumerable<Bank>>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (BankList) GetHeaders(bankList, result);

            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }

            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }


        public async Task<GatewayString> FindFlowIdAsync(string queryString, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var result = await client.GetAsync("ob/findFlowId?queryString=" + HttpUtility.UrlEncode(queryString));

            if (result.IsSuccessStatusCode)
            { 
                GatewayString gatewayString = new GatewayString();
                gatewayString.Content=await result.Content.ReadAsStringAsync();
                return (GatewayString) GetHeaders(gatewayString, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        #endregion

        #region PIS

        public async Task<BankPaymentAccessOption> GetBankPaymentAccessOptionsAsync(int connectorId, string? XRequestID=null)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var result = await client.GetAsync("ob/pis/payments/options/" + connectorId.ToString());

            if (result.IsSuccessStatusCode)
            {
                BankPaymentAccessOption bankPaymentAccessOption = JsonConvert.DeserializeObject<BankPaymentAccessOption>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (BankPaymentAccessOption)GetHeaders(bankPaymentAccessOption,result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<PaymentInitResponse> PaymentInitiateAsync(PaymentInitRequest paymentInitRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(paymentInitRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/payments", stringContent);

            if (result.IsSuccessStatusCode)
            {
                
                PaymentInitResponse paymentInitResponse =  JsonConvert.DeserializeObject<PaymentInitResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });

                return (PaymentInitResponse)GetHeaders(paymentInitResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<PaymentFinalizeResponse> PaymentFinalizeAsync(PaymentFinalizeRequest paymentFinalizeRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(paymentFinalizeRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/pis/payments", stringContent);

            if (result.IsSuccessStatusCode)
            {
                PaymentFinalizeResponse paymentFinalizeResponse= JsonConvert.DeserializeObject<PaymentFinalizeResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (PaymentFinalizeResponse)GetHeaders(paymentFinalizeResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<PaymentStatusResponse> PaymentStatusAsync(PaymentStatusRequest paymentStatusRequest, string? XRequestID=null)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(paymentStatusRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/payments/status", stringContent);

            if (result.IsSuccessStatusCode)
            {
                PaymentStatusResponse paymentStatusResponse= JsonConvert.DeserializeObject<PaymentStatusResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (PaymentStatusResponse)GetHeaders(paymentStatusResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        public async Task<AccountsForPaymentResponseInit> GetAccountsForPaymentsInitAsync(AccountsForPaymentRequestInit accountsForPaymentRequestInit, string? XRequestID=null)
        {
           
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(accountsForPaymentRequestInit), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/accounts", stringContent);

            if (result.IsSuccessStatusCode)
            {
                AccountsForPaymentResponseInit accountsForPaymentResponseInit =  JsonConvert.DeserializeObject<AccountsForPaymentResponseInit>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (AccountsForPaymentResponseInit)GetHeaders(accountsForPaymentResponseInit, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        public async Task<AccountsForPaymentFinalize> GetAccountsForPaymentsFinalizeAsync(AccountsForPaymentFinalizeRequest accountsForPaymentFinalizeRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(accountsForPaymentFinalizeRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/pis/accounts", stringContent);

            if (result.IsSuccessStatusCode)
            {
                AccountsForPaymentFinalize accountsForPaymentFinalize= JsonConvert.DeserializeObject<AccountsForPaymentFinalize>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (AccountsForPaymentFinalize)GetHeaders(accountsForPaymentFinalize, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        #endregion

        #region BULKPIS
        
        public async Task<BulkPaymentInitResponse> BulkPaymentInitiateAsync(BulkPaymentInitRequest bulkPaymentInitRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(bulkPaymentInitRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/payments/bulk", stringContent);

            if (result.IsSuccessStatusCode)
            {
                BulkPaymentInitResponse bulkPaymentInitResponse= JsonConvert.DeserializeObject<BulkPaymentInitResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (BulkPaymentInitResponse)GetHeaders(bulkPaymentInitResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                //Bad Request or Pre-conditions failed.
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<BulkPaymentFinalizeResponse> BulkPaymentFinalizeAsync(BulkPaymentFinalizeRequest bulkPaymentFinalizeRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(bulkPaymentFinalizeRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/pis/payments/bulk", stringContent);

            if (result.IsSuccessStatusCode)
            {
                BulkPaymentFinalizeResponse bulkPaymentFinalizeResponse= JsonConvert.DeserializeObject<BulkPaymentFinalizeResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (BulkPaymentFinalizeResponse)GetHeaders(bulkPaymentFinalizeResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<BulkPaymentStatusResponse> BulkPaymentStatusAsync(BulkPaymentStatusRequest bulkPaymentStatusRequest, string? XRequestID=null)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(bulkPaymentStatusRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/pis/payments/bulk/status", stringContent);

            if (result.IsSuccessStatusCode)
            {
                BulkPaymentStatusResponse bulkPaymentStatusResponse= JsonConvert.DeserializeObject<BulkPaymentStatusResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (BulkPaymentStatusResponse)GetHeaders(bulkPaymentStatusResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        /// <param name="XRequestID"></param>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<BankAccessOption> GetBankAccessOptionsAsync(int connectorId, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            
            client = SetHeaders(client, XRequestID);
            
            var result = await client.GetAsync("ob/ais/access/options/" + connectorId.ToString());

            if (result.IsSuccessStatusCode)
            {
                
                BankAccessOption bankAccessOption =JsonConvert.DeserializeObject<BankAccessOption>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                
                return (BankAccessOption) GetHeaders((IBase)bankAccessOption, result);

            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        public async Task<BankAccessResponse> RequestBankAccessAsync(BankAccessRequest bankAccessRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(bankAccessRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/ais/access", stringContent);

            if (result.IsSuccessStatusCode)
            {
                BankAccessResponse bankAccessResponse= JsonConvert.DeserializeObject<BankAccessResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (BankAccessResponse)GetHeaders(bankAccessResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        public async Task<BankAccessResponseFinalize> FinalizeRequestBankAccessAsync(BankAccessRequestFinalize bankAccessRequestFinalize, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(bankAccessRequestFinalize), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/ais/access", stringContent);

            if (result.IsSuccessStatusCode)
            {
                BankAccessResponseFinalize bankAccessResponseFinalize= JsonConvert.DeserializeObject<BankAccessResponseFinalize>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (BankAccessResponseFinalize)GetHeaders(bankAccessResponseFinalize, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        public async Task<GatewayBool> CancelBankAccessAsync(DeleteConsentRequest deleteConsentRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(deleteConsentRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/ais/consents/delete", stringContent);

            if (result.IsSuccessStatusCode)
            {
                GatewayBool gatewayBool = new GatewayBool();
                gatewayBool.value = true;
                return (GatewayBool)GetHeaders(gatewayBool, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        public async Task<GatewayBool> RemoveBankAccountAsync(DeleteAccountRequest deleteAccountRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(deleteAccountRequest), Encoding.UTF8, "application/json");

            var result = await client.PutAsync("ob/ais/accounts/delete", stringContent);

            if (result.IsSuccessStatusCode)
            {
                GatewayBool gatewayBool = new GatewayBool();
                gatewayBool.value = true;
                return (GatewayBool)GetHeaders(gatewayBool, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<BankAccountsResponse> GetBankAccountsAsync(BankAccountsRequest bankAccountsRequest, string? XRequestID=null)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(bankAccountsRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync("ob/ais/accounts", stringContent);

            if (result.IsSuccessStatusCode)
            {
                BankAccountsResponse bankAccountsResponse = JsonConvert.DeserializeObject<BankAccountsResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (BankAccountsResponse)GetHeaders(bankAccountsResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        #endregion

        #region AIS-TRANSACTIONS

        public async Task<BalanceResponse> GetBalancesAsync(string accountId, BalanceRequest balanceRequest, string? XRequestID=null)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(balanceRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync($"ob/ais/accounts/{accountId}/balances", stringContent);

            if (result.StatusCode==HttpStatusCode.OK)
            {
                BalanceResponse balanceResponse = JsonConvert.DeserializeObject<BalanceResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (BalanceResponse)GetHeaders(balanceResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<TransactionResponse> GetTransactionsAsync(string accountId, TransactionRequest transactionRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(transactionRequest), Encoding.UTF8, "application/json");

            HttpResponseMessage result = await client.PostAsync($"ob/ais/accounts/{accountId}/transactions", stringContent);

            if (result.StatusCode==HttpStatusCode.OK || result.StatusCode == HttpStatusCode.PartialContent)
            {
                TransactionResponse transactionResponse = JsonConvert.DeserializeObject<TransactionResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                if (result.StatusCode == HttpStatusCode.PartialContent)
                    transactionResponse.isConsentLost = true;

                return (TransactionResponse)GetHeaders(transactionResponse, result);
            }

            if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
            {
                throw new GatewayException(await GetGWError(result));
            }
            throw new Exception(result.StatusCode + " " + result.ReasonPhrase + " " + await result.Content.ReadAsStringAsync());
        }

        public async Task<TransactionResponse> GetTransactionsNextAsync(string accountId, TransactionPagingRequest transactionRequest, string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(transactionRequest), Encoding.UTF8, "application/json");

            var result = await client.PostAsync($"ob/ais/accounts/{accountId}/transactions/next", stringContent);

            if (result.StatusCode==HttpStatusCode.OK)
            {
                TransactionResponse transactionResponse= JsonConvert.DeserializeObject<TransactionResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                if (result.StatusCode == HttpStatusCode.PartialContent)
                    transactionResponse.isConsentLost = true;
                return (TransactionResponse)GetHeaders(transactionResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        public async Task<TermsDTO> GetTCAsync(string language="", string? XRequestID=null)
        {
            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var result = await client.GetAsync("ob/gw/tc/latest?language=" + language);

            if (result.IsSuccessStatusCode)
            {
                TermsDTO termsDto =  JsonConvert.DeserializeObject<TermsDTO>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (TermsDTO)GetHeaders(termsDto, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        public async Task<TermsValidated> GetTCLatestAsync(string psuId, string? XRequestID=null)
        {
            TermsValidated termsValidatedDTO = new()
            {
                psuId = psuId,
                version = -1
            };

            if (string.IsNullOrEmpty(psuId))
                return termsValidatedDTO;

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var result = await client.GetAsync($"ob/gw/users/{psuId}/tc/latest" );

            if (result.IsSuccessStatusCode)
            {
                TermsValidated termsValidated= JsonConvert.DeserializeObject<TermsValidated>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (TermsValidated)GetHeaders(termsValidated, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
        public async Task<UserRegisterResponse> CreateUserAsync(UserDTO userDTO, string? XRequestID=null)
        {

            var client = _httpClientFactory.CreateClient("BankingSdkGatewayClient");
            client = SetHeaders(client, XRequestID);
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("ob/gw/users", stringContent);

            if (result.IsSuccessStatusCode)
            {
                UserRegisterResponse userRegisterResponse= JsonConvert.DeserializeObject<UserRegisterResponse>(await result.Content.ReadAsStringAsync(), new JsonSerializerSettings
                {
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
                return (UserRegisterResponse)GetHeaders(userRegisterResponse, result);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest || (int)result.StatusCode == 422)
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
