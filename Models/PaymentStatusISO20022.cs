using System;
using System.Collections.Generic;
using System.Text;

namespace Exthand.GatewayClient.Models
{
    //any new Status has to be added at the ond of the enum 
    public enum PaymentStatusISO20022
    {
        /// <summary>
        /// AcceptedSettlementCompleted
        /// </summary>
        ACCC,
        /// <summary>
        /// AcceptedCustomerProfile
        /// </summary>
        ACCP,
        /// <summary>
        /// AcceptedSettlementCompleted
        /// </summary>
        ACSC,
        /// <summary>
        /// AcceptedSettlementInProcess
        /// </summary>
        ACSP,
        /// <summary>
        /// AcceptedTechnicalValidation
        /// </summary>
        ACTC,
        /// <summary>
        /// AcceptedWithChange
        /// </summary>
        ACWC,
        /// <summary>
        /// AcceptedWithoutPosting
        /// </summary>
        ACWP,
        /// <summary>
        /// Pending
        /// </summary>
        PDNG,
        /// <summary>
        /// Received
        /// </summary>
        RCVD,
        /// <summary>
        /// Rejected
        /// </summary>
        RJCT,
        /// <summary>
        /// Unknown
        /// </summary>
        UNKN,
        /// <summary>
        /// AcceptedFundsChecked
        /// </summary>
        ACFC,
        /// <summary>
        /// Cancelled
        /// </summary>
        CANC,
        /// <summary>
        /// PartiallyAcceptedTechnicalCorrect
        /// </summary>
        PATC,
        /// <summary>
        /// Presented
        /// </summary>
        PRES,
        /// <summary>
        /// Blocked
        /// </summary>
        BLCK
    }
}
