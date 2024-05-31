namespace Exthand.GatewayClient.Models
{
    public enum StatusInfo
    {
        /// <summary>
        /// The bank has the default behavior
        /// </summary>
        DEFAULT,
        /// <summary>
        /// The bank will not callback after this call. You should open the provided url on a new page/tab
        /// </summary>
        NO_CALLBACK
    }

}