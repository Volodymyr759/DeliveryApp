using System;

namespace Delivery.DAL.Models
{
    /// <summary>
    /// Відправлення, відстежуване користувачем. Передбачається, що існує в інформаційній системі поштового оператора
    /// </summary>
    public interface IInvoice
    {
        /// <summary>
        /// Ідентифікатор відправлення в базі даних сервісу Delivery
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Ідентифікатор користувача в базі даних сервісу Delivery
        /// </summary>
        string AccountUserId { get; set; }

        /// <summary>
        /// Ідентифікатор поштового оператора
        /// </summary>
        int PostOperatorId { get; set; }

        /// <summary>
        /// Номер відправлення в інформаційній системі одного з поштових операторів
        /// </summary>
        string Number { get; set; }

        /// <summary>
        /// Дата відправлення за даними інформаційної системи поштового оператора
        /// </summary>
        DateTime SendDateTime { get; set; }

        /// <summary>
        /// Відправник
        /// </summary>
        string Sender { get; set; }

        /// <summary>
        /// Адреса відправника
        /// </summary>
        string SenderAddress { get; set; }

        /// <summary>
        /// Одержувач
        /// </summary>
        string Recipient { get; set; }

        /// <summary>
        /// Адреса одержувача
        /// </summary>
        string RecipientAddress { get; set; }

        /// <summary>
        /// Адреса поточного місцезнаходження відправлення
        /// </summary>
        string CurrentLocation { get; set; }

        /// <summary>
        /// Поточний статус відправлення
        /// </summary>
        string ActualStatus { get; set; }

        /// <summary>
        /// Примітки
        /// </summary>
        string Notes { get; set; }
    }
}