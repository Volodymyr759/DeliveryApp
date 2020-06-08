using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Клас відправлення, пристосований до json-формату відповіді API-сервісу Нової Пошти
    /// </summary>
    public class ApiInvoicesModel
    {
        /// <summary>
        /// Номер відправлення
        /// </summary>
        public string IntDocNumber { get; set; }

        /// <summary>
        /// Відділення одержувача
        /// </summary>
        public string RecipientAddressDescription { get; set; }

        /// <summary>
        /// Місто одержувача
        /// </summary>
        public string CityRecipientDescription { get; set; }

        /// <summary>
        /// Дата створення відправлення
        /// </summary>
        public string Datetime { get; set; }

        /// <summary>
        /// Оголошена вартість відправлення
        /// </summary>
        public string CostOnSite { get; set; }

        /// <summary>
        /// Тип доставки
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Вага
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Опис
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Вид оплати: готівка, б/г
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Платник: відправник або одержувач
        /// </summary>
        public string PayerType { get; set; }

        /// <summary>
        /// Статус, наприклад: Замовлення в обробці
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// Телефон відправника
        /// </summary>
        public string SendersPhone { get; set; }

        /// <summary>
        /// Відділення відправника
        /// </summary>
        public string SenderAddressDescription { get; set; }

        /// <summary>
        /// Місто відправника
        /// </summary>
        public string CitySenderDescription { get; set; }
        
    }
}
