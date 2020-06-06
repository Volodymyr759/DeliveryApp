namespace Delivery.Web.Models
{
    /// <summary>
    /// Інтерфейс класу повідомлень користувача у системі Delivery
    /// </summary>
    public interface IDeliveryMessage
    {
        /// <summary>
        /// Заголовок повідомлення
        /// </summary>
        string Body { get; set; }

        /// <summary>
        /// Тіло повідомлення
        /// </summary>
        string Title { get; set; }
    }
}