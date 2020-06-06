using System;

namespace Delivery.DAL.Models
{
    /// <summary>
    /// Відправлення, відстежуване користувачем. Передбачається, що існує в інформаційній системі поштового оператора
    /// </summary>
    public class Invoice : IInvoice
    {
        /// <summary>
        /// Ідентифікатор відправлення в базі даних сервісу Delivery
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ідентифікатор користувача в базі даних сервісу Delivery
        /// </summary>
        public string AccountUserId { get; set; }

        /// <summary>
        /// Ідентифікатор поштового оператора
        /// </summary>
        public int PostOperatorId { get; set; }

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
