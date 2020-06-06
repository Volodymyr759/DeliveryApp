using System.Collections.Generic;
using Delivery.DAL.Models;

namespace Delivery.DAL.Repositories
{
    /// <summary>
    /// Репозиторій відправлень користувача
    /// </summary>
    public interface IInvoicesRepository
    {
        /// <summary>
        /// Створює нове відправлення користувача
        /// </summary>
        /// <param name="invoice">Екземпляр відправлення</param>
        void Create(IInvoice invoice);

        /// <summary>
        /// Повертає список усіх відправлень, існуючих на сервісі Delivery
        /// </summary>
        /// <returns>Список відправлень</returns>
        IEnumerable<IInvoice> GetAll();

        /// <summary>
        /// Повертає відправлення по ідентифікатору
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        /// <returns>Екземпляр відправлення</returns>
        IInvoice GetById(int invoiceId);

        /// <summary>
        /// Повертає список відправлень користувача
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Список відправлень користувача</returns>
        IEnumerable<IInvoice> GetByUserId(string userId);

        /// <summary>
        /// Повертає ідентифікатор поштового оператора за назвою
        /// </summary>
        /// <param name="postOperatorName">Назва поштового оператора</param>
        /// <returns>Ідентифікатор поштового оператора</returns>
        int GetPostOperatorIdByName(string postOperatorName);

        /// <summary>
        /// Повертає назву поштового оператора по ідентифікатору
        /// </summary>
        /// <param name="postOperatorId">Ідентифікатор поштового оператора</param>
        /// <returns>Назва поштового оператора</returns>
        string GetPostOperatorNameById(int postOperatorId);

        /// <summary>
        /// Видаляє відправлення по ідентифікатору відправлення
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        void Delete(int invoiceId);

        /// <summary>
        /// Видаляє усі відправлення по ідентифікатору користувача
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        void DeleteByUserId(string userId);

        /// <summary>
        /// Оновлює поточний статус відправлення
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        /// <param name="actualStatus">Поточний статус</param>
        void UpdateStatus(int invoiceId, string actualStatus);
    }
}