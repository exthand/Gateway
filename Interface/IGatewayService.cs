

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Exthand.GatewayClient.Models;

namespace Exthand.GatewayClient.Interface
{
    public interface IGatewayService
    {
        Task<GatewayBool> CancelBankAccessAsync(DeleteConsentRequest deleteConsentRequest, string? XRequestID=null);
        Task<UserRegisterResponse> CreateUserAsync(UserDTO userDTO, string? XRequestID=null);
        Task<BankAccessResponseFinalize> FinalizeRequestBankAccessAsync(BankAccessRequestFinalize bankAccessRequestFinalize, string? XRequestID=null);
        Task<GatewayString> FindFlowIdAsync(string queryString, string? XRequestID=null);
        Task<BalanceResponse> GetBalancesAsync(string accountId, BalanceRequest balanceRequest, string? XRequestID=null);
        Task<BankAccessOption> GetBankAccessOptionsAsync(int connectorId, string XRequestID = null);
        Task<BankAccountsResponse> GetBankAccountsAsync(BankAccountsRequest bankAccountsRequest, string? XRequestID=null);
        Task<BankPaymentAccessOption> GetBankPaymentAccessOptionsAsync(int connectorId, string? XRequestID=null);
        Task<BankList> GetBanksAsync(string countryCode, string? XRequestID=null);
        Task<TermsDTO> GetTCAsync(string language = "", string? XRequestID=null);
        Task<TermsValidated> GetTCLatestAsync(string psuId, string? XRequestID=null);
        Task<TransactionResponse> GetTransactionsAsync(string accountId, TransactionRequest transactionRequest, string? XRequestID=null);
        Task<TransactionResponse> GetTransactionsNextAsync(string accountId, TransactionPagingRequest transactionRequest, string? XRequestID=null);
        Task<PaymentFinalizeResponse> PaymentFinalizeAsync(PaymentFinalizeRequest paymentFinalizeRequest, string? XRequestID=null);
        Task<PaymentInitResponse> PaymentInitiateAsync(PaymentInitRequest paymentInitRequest, string? XRequestID=null);
        Task<PaymentStatusResponse> PaymentStatusAsync(PaymentStatusRequest paymentStatusRequest, string? XRequestID=null);

        Task<BulkPaymentInitResponse> BulkPaymentInitiateAsync(BulkPaymentInitRequest bulkPaymentInitRequest,
            string? XRequestID = null);        
        Task<BulkPaymentFinalizeResponse> BulkPaymentFinalizeAsync(
            BulkPaymentFinalizeRequest bulkPaymentFinalizeRequest, string? XRequestID = null);
        Task<BulkPaymentStatusResponse> BulkPaymentStatusAsync(BulkPaymentStatusRequest bulkPaymentStatusRequest,
            string? XRequestID = null);
        
        Task<GatewayBool> RemoveBankAccountAsync(DeleteAccountRequest deleteAccountRequest, string? XRequestID=null);
        Task<BankAccessResponse> RequestBankAccessAsync(BankAccessRequest bankAccessRequest, string? XRequestID=null);

        Task<AccountsForPaymentResponseInit> GetAccountsForPaymentsInitAsync(AccountsForPaymentRequestInit accountsForPaymentRequestInit, string? XRequestID=null);
        Task<AccountsForPaymentFinalize> GetAccountsForPaymentsFinalizeAsync(AccountsForPaymentFinalizeRequest accountsForPaymentFinalizeRequest, string? XRequestID=null);

    }
}
