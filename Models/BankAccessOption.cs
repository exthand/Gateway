using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public class BankAccessOption
    {

        public int accessOption { get; set; }
        public int currencyOption { get; set; }
        public int linkingOption { get; set; }

        public List<AccountSchemeType> AccountSchemes { get; set; } = new List<AccountSchemeType>() { AccountSchemeType.IBAN };
        public List<AdditionalPropertyRequested> AdditionalPropertiesRequested { get; set; } = new List<AdditionalPropertyRequested>();
    }
}
