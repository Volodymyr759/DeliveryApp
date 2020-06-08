using System;
using System.Threading.Tasks;
using Delivery.BLL.DTO;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Html-парсер поштового оператора
    /// </summary>
    public class HtmlSearcherAgent : ISearchAgent
    {
        private readonly string name = "Укрпошта";

        /// <summary>
        /// Повертає назву пошукового агента, якого реалізовано в системі Delivery
        /// </summary>
        /// <returns>Назва пошукового агента</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Шукає поштове відправлення по номеру і повертає актуальний статус з інформаційно системи поштового оператора
        /// </summary>
        /// <param name="number">Номер відправлення</param>
        /// <returns>Актуальний статус відправлення</returns>
        public async Task<string> GetStatus(string number)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Шукає поштове відправлення по номеру і повертає Dto модель, включаючи назву оператора
        /// </summary>
        /// <param name="number">Номер відправлення в інформаційній системі поштового оператора</param>
        /// <returns>Екземпляр Dto моделі відправлення</returns>
        public InvoiceDto SearchByNumber(string number)
        {
            throw new NotImplementedException();
        }
    }
}
