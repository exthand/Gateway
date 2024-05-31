using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class AccountsForPaymentResponseInit : IBase
    {
        public string XRequestID { get; set; }
        public string XCorrelationID { get; set; }
        public string XOperationID { get; set; }
        
        /// <summary>
        /// Status of the call
        /// </summary>
        public ResultStatus ResultStatus { get; set; }
        /// <summary>
        /// Returns more information about the bank's behevior for the given result
        /// </summary>
        public StatusInfo StatusInfo { get; set; }
        /// <summary>
        /// Data to be used in the next step. Depends on the status.
        /// </summary>
        public string DataString { get; set; }
        /// <summary>
        /// List of options to choose from in case of ResultStatus = **SELECT_OPTION (6)**
        /// </summary>
        public Dictionary<string, string> Options { get; set; }
        /// <summary>
        /// Flow context to be used in the next step
        /// </summary>
        public string FlowContext { get; set; }
        /// <summary>
        /// If user context changed, new is returned. Otherwise null.
        /// </summary>
        public string UserContext { get; set; }
        /// <summary>
        /// Raw data from the bank
        /// </summary>
        public string RawResponse { get; set; }

    }


}
