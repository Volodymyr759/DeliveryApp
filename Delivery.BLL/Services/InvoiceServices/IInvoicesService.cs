using Delivery.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Сервіс управління посилками/відправленнями
    /// </summary>
   public interface IInvoicesService
    {

        /// <summary>
        /// Створення нового відправлення користувача
        /// </summary>
        /// <param name="userId">Ідентифікатр користувача</param>
        /// <param name="number">Номер відправлення</param>
        void Add(string userId, string number);

        /// <summary>
        /// Повертає усі створені користувачами відправлення в сервісі Delivery
        /// </summary>
        /// <returns>Список відправлень</returns>
        IEnumerable<InvoiceDto> GetAll();

        /// <summary>
        /// Повертає відправлення по ідентифікатору
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        /// <returns>Модель Dto відправлення</returns>
        InvoiceDto GetById(int invoiceId);

        /// <summary>
        /// Повертає список відправлень обраного користувача
        /// </summary>
        /// <param name="userId">Ідентифікатр користувача</param>
        /// <returns></returns>
        IEnumerable<InvoiceDto> GetInvoicesByUserId(string userId);

        /// <summary>
        /// Видалення відправлення
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        void Remove(int invoiceId);

        /// <summary>
        /// Видалення відправлень користувача
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        void RemoveByUser(string userId);

        /// <summary>
        /// Пошук відправлення по номеру
        /// </summary>
        /// <param name="number">Номер відправлення в інформаційній системі поштового оператора</param>
        /// <returns>Модель Dto відправлення</returns>
        InvoiceDto SearchByNumber(string number);

        /// <summary>
        /// Оновлює статус поштового відправлення
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        void UpdateStatus(int invoiceId);
    }
}
