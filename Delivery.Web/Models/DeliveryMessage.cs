namespace Delivery.Web.Models
{
    /// <summary>
    /// Клас повідомлень користувача у системі Delivery
    /// </summary>
    public class DeliveryMessage : IDeliveryMessage
    {
        /// <summary>
        /// Заголовок повідомлення
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Тіло повідомлення
        /// </summary>
        public string Body { get; set; }
    }
}