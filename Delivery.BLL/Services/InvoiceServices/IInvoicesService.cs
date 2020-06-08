using Delivery.BLL.DTO;
using System.Collections.Generic;
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
        /// <param name="apiKeys">Ключі доступу до реалізованих Api-сервісів</param>
        void Add(string userId, string number, Dictionary<string, string> apiKeys);

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
        /// <param name="apiKeys">Ключі доступу до реалізованих Api-сервісів</param>
        /// <returns>Модель Dto відправлення</returns>
        InvoiceDto SearchByNumber(string number, Dictionary<string, string> apiKeys);

        /// <summary>
        /// Оновлює статус поштового відправлення
        /// </summary>
        /// <param name="invoiceId">Ідентифікатор відправлення</param>
        /// <param name="apiKeys">Ключі доступу до реалізованих Api-сервісів</param>
        Task UpdateStatusAsync(int invoiceId, Dictionary<string, string> apiKeys);
    }
}
