using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.BLL.DTO
{
    /// <summary>
    /// Dto модель відправлення користувача
    /// </summary>
    public class InvoiceDto
    {
        /// <summary>
        /// Ідентифікатор відправлення
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Назва поштового оператора
        /// </summary>
        public string PostOperatorName { get; set; }

        /// <summary>
        /// Шлях до розташування зображення лого поштового оператора на хост-сервері сервісу Delivery
        /// </summary>
        public string PathToLogoImage { get; set; }

        /// <summary>
        /// Номер відправлення в інформаційній системі одного з поштових операторів
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата відправлення за даними інформаційної системи поштового оператора
        /// </summary>
        public DateTime SendDateTime { get; set; }

        /// <summary>
        /// Відправник
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Адреса відправника
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        /// Одержувач
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Адреса одержувача
        /// </summary>
        public string RecipientAddress { get; set; }

        /// <summary>
        /// Адреса поточного місцезнаходження відправлення
        /// </summary>
        public string CurrentLocation { get; set; }

        /// <summary>
        /// Поточний статус відправлення
        /// </summary>
        public string ActualStatus { get; set; }

        /// <summary>
        /// Примітки
        /// </summary>
        public string Notes { get; set; }
    }
}
