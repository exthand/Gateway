

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
        Task<bool> CancelBankAccessAsync(DeleteConsentRequest deleteConsentRequest);
        Task<UserRegisterResponse> CreateUserAsync(UserDTO userDTO);
        Task<BankAccessResponseFinalize> FinalizeRequestBankAccessAsync(BankAccessRequestFinalize bankAccessRequestFinalize);
        Task<string> FindFlowIdAsync(string queryString);
        Task<BalanceResponse> GetBalancesAsync(string accountId, BalanceRequest balanceRequest);
        Task<BankAccessOption> GetBankAccessOptionsAsync(int connectorId);
        Task<BankAccountsResponse> GetBankAccountsAsync(BankAccountsRequest bankAccountsRequest);
        Task<BankPaymentAccessOption> GetBankPaymentAccessOptionsAsync(int connectorId);
        Task<IEnumerable<Bank>> GetBanksAsync(string countryCode);
        Task<TermsDTO> GetTCAsync(string language = "");
        Task<TermsValidated> GetTCLatestAsync(string psuId);
        Task<TransactionResponse> GetTransactionsAsync(string accountId, TransactionRequest transactionRequest);
        Task<TransactionResponse> GetTransactionsNextAsync(string accountId, TransactionPagingRequest transactionRequest);
        Task<PaymentFinalizeResponse> PaymentFinalizeAsync(PaymentFinalizeRequest paymentFinalizeRequest);
        Task<PaymentInitResponse> PaymentInitiateAsync(PaymentInitRequest paymentInitRequest);
        Task<PaymentStatusResponse> PaymentStatusAsync(PaymentStatusRequest paymentStatusRequest);
        Task<bool> RemoveBankAccountAsync(DeleteAccountRequest deleteAccountRequest);
        Task<BankAccessResponse> RequestBankAccessAsync(BankAccessRequest bankAccessRequest);

        Task<PaymentFinalizeResponse> GetAccountsForPaymentsInitAsync(AccountsForPaymentRequestInit accountsForPaymentRequestInit);
        Task<AccountsForPaymentFinalize> GetAccountsForPaymentsFinalizeAsync(AccountsForPaymentFinalizeRequest accountsForPaymentFinalizeRequest);

    }
}
