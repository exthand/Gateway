using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class AccountsAccessRequest
    {
        private List<BankAccount> _transactionAccounts;
        private List<BankAccount> _balanceAccounts;

        public Dictionary<String, String> additionalProperties { get; set; } = new Dictionary<string, string>();

        public string flowId { get; set; }
        public string redirectUrl { get; set; }
        /// <summary>
        /// Maximum 4
        /// </summary>
        public int frequencyPerDay { get; set; } = 4;
        public string psuIp { get; set; }
        public string psuIpPort { get; set; }
        public string psuAccept { get; set; }
        public string psuAcceptCharset { get; set; }
        public string psuAcceptEncoding { get; set; }
        public string psuAcceptLanguage { get; set; }
        public string psuDate { get; set; }
        public string psuDeviceId { get; set; }
        public string psuGeoLocation { get; set; }
        public string psuHttpMethod { get; set; }
        public string psuLastLoggedTime { get; set; }
        public string psuReferer { get; set; }
        public string psuUserAgent { get; set; }
        public BankAccount singleAccount { get; set; }
        public List<BankAccount> transactionAccounts
        {
            get
            {
                if (_transactionAccounts == null || !_transactionAccounts.Any())
                {
                    return null;
                }
                else
                {
                    return _transactionAccounts;
                }
            }
            set
            {
                _transactionAccounts = value;
            }
        }
        public List<BankAccount> balanceAccounts
        {
            get
            {
                if (_balanceAccounts == null || !_balanceAccounts.Any())
                {
                    return null;
                }
                else
                {
                    return _balanceAccounts;
                }
            }
            set
            {
                _balanceAccounts = value;
            }
        }
    }
}
