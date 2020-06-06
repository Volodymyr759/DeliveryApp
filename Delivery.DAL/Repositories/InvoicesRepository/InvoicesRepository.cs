using Delivery.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.DAL.Repositories
{
    /// <summary>
    /// Репозиторій відправлень користувача
    /// </summary>
    public class InvoicesRepository : IInvoicesRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString">Строка підключення</param>
        public InvoicesRepository(string connectionString) => this.connectionString = connectionString;

        /// <summary>
        /// Створює нове відправлення користувача
        /// </summary>
        /// <param name="invoice">Екземпляр відправлення</param>
        public void Create(IInvoice invoice)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Повертає список усіх відправлень, існуючих на сервісі Delivery
        /// </summary>
        /// <returns>Список відправлень</returns>
        public IEnumerable<IInvoice> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Повертає відправлення по ідентифікатору
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        /// <returns>Екземпляр відправлення</returns>
        public IInvoice GetById(int invoiceId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Повертає список відправлень користувача, існуючих на сервісі Delivery
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Список відправлень користувача</returns>
        public IEnumerable<IInvoice> GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Повертає назву поштового оператора по ідентифікатору
        /// </summary>
        /// <param name="postOperatorId">Ідентифікатор поштового оператора</param>
        /// <returns>Назва поштового оператора</returns>
        public string GetPostOperatorNameById(int postOperatorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Повертає ідентифікатор поштового оператора за назвою
        /// </summary>
        /// <param name="postOperatorName">Назва поштового оператора</param>
        /// <returns>Ідентифікатор поштового оператора</returns>
        public int GetPostOperatorIdByName(string postOperatorName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Видаляє відправлення користувача
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        public void Delete(int invoiceId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Видаляє усі відправлення по ідентифікатору користувача
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        public void DeleteByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Оновлює поточний статус відправлення
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        /// <param name="actualStatus">Поточний статус</param>
        public void UpdateStatus(int invoiceId, string actualStatus)
        {
            throw new NotImplementedException();
        }
    }
}
