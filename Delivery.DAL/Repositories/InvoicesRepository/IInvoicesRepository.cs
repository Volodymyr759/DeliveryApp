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
        /// Повертає словник ідентифікатор:назва поштових операторів, реалізованих в системі Delivery і внесених адміністратором в базу даних
        /// </summary>
        /// <returns>словник ідентифікатор:назва поштових операторів</returns>
        Dictionary<int, string> GetPostOperatorsIdNames();

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