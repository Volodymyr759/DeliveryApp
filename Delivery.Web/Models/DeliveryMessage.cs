namespace Delivery.Web.Models
{
    /// <summary>
    /// User message class in the Delivery system
    /// </summary>
    public class DeliveryMessage : IDeliveryMessage
    {

        /// <summary>
        /// Message body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Message title
        /// </summary>
        public string Title { get; set; }
    }
}