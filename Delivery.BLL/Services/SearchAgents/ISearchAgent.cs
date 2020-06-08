using Delivery.BLL.DTO;
using System.Threading.Tasks;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Інтерфейс пошукових агентів
    /// </summary>
    public interface ISearchAgent
    {
        /// <summary>
        /// Повертає назву пошукового агента, якого реалізовано в системі Delivery
        /// </summary>
        /// <returns>Назва пошукового агента</returns>
        string GetName();

        /// <summary>
        /// Шукає поштове відправлення по номеру і повертає актуальний статус з інформаційно системи поштового оператора
        /// </summary>
        /// <param name="number">Номер відправлення</param>
        /// <returns>Актуальний статус відправлення</returns>
        Task<string> GetStatus(string number);

        /// <summary>
        /// Шукає поштове відправлення по номеру і повертає Dto модель, включаючи назву оператора
        /// </summary>
        /// <param name="number">Номер відправлення в інформаційній системі поштового оператора</param>
        /// <returns>Екземпляр Dto моделі відправлення</returns>
        InvoiceDto SearchByNumber(string number);
    }
}
