using System.Collections.Generic;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Клас для створення пошукових агентів фабричним методом
    /// </summary>
    public class FactoryOfAgents
    {
        /// <summary>
        /// Повертає список реалізованих в системі Delivery пошукових агентів
        /// </summary>
        /// <param name="apiKeys">Ключі доступу до реалізованих Api-сервісів</param>
        /// <returns>Список пошукових агентів</returns>
        public static IEnumerable<ISearchAgent> GetAllAgents(Dictionary<string, string> apiKeys)
        {
            var listOfAgents = new List<ISearchAgent>
            {
                new ApiSearcherAgent(apiKeys["ApiKeyNovaPoshta"]),
                new HtmlSearcherAgent()
            };

            return listOfAgents;
        }
    }
}
