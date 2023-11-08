using System;
namespace Exthand.Gateway
{
	public static class BalanceType
	{
        /// <summary>
        /// Balance is closed available. Available amount is certain the day before at 23.59.59
        /// </summary>
		public static readonly string CLAV = "CLAV";

        /// <summary>
        /// Balance is closed booked. Value is certain the day before at 23.59.59
        /// </summary>
        public static readonly string CLBD = "CLBD";

        /// <summary>
        /// Opening booked balance. Value is certain this day at 00.00.00
        /// </summary>
        public static readonly string OPBD = "OPBD";

        public static readonly string OTHR = "OTHR";
        public static readonly string INFO = "INFO";

        /// <summary>
        /// Balance is intermediary booked. Value is certain a the moment of reception.
        /// </summary>
        public static readonly string ITBD = "ITBD";

        /// <summary>
        /// Balance is intermediary available. Value is certain a the moment of reception.
        /// </summary>
        public static readonly string ITAV = "ITAV";

        /// <summary>
        /// Balance is expected. Value is uncertain a the moment of reception.
        /// </summary>
        public static readonly string XPCD = "XPCD";
    }
}

