using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{

    /// <summary>
    /// Added v4.0.6
    /// All connectors with status = 0 are production ready and tested.
    /// </summary>
    public enum BankStatus : int
    {
        PRODUCTION = 0,
        TESTING = 1,
        ERROR = 2,
        BANK_ERROR = 3,
        UNAVAILABLE = 4,
        DELETED = 98,
        DEV_ONGOING = 99
    }


    public class Bank
    {

        public string fullname { get; set; }

        public int connectorID { get; set; }

        public string logoURL { get; set; }

        public string countryISO { get; set; }

        public BankStatus status { get; set; }

        /// <summary>
        /// Grouping name.
        /// Added v4.0.6
        /// </summary>
        public string bankGroup { get; set; }

    }
}
