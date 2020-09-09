namespace Delivery.Web.Models
{
    /// <summary>
    /// User message class interface in the Delivery system
    /// </summary>
    public interface IDeliveryMessage
    {
        /// <summary>
        /// Message body
        /// </summary>
        string Body { get; set; }

        /// <summary>
        /// Message title
        /// </summary>
        string Title { get; set; }
    }
}