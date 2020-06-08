using System;

namespace Delivery.Web.Models
{
    /// <summary>
    /// ViewModel модель відправлення користувача
    /// </summary>
    public class InvoiceViewModel
    {
        /// <summary>
        /// Ідентифікатор відправлення
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Номер відправлення в інформаційній системі поштового оператора
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Назва поштового оператора
        /// </summary>
        public string PostOperatorName { get; set; }

        /// <summary>
        /// Дата відправлення за даними інформаційної системи поштового оператора
        /// </summary>
        public DateTime SendDateTime { get; set; }

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