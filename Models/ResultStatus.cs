using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exthand.GatewayClient.Models
{
    public enum ResultStatus : int
    {
        UNKNOW = 0,
        DONE = 1,
        REDIRECT =2,
        DECOUPLED = 3,
        PASSWORD = 4,
        MORE_INFO = 5,
        SELECT_OPTION = 6,
        ERROR=7
    }
}
