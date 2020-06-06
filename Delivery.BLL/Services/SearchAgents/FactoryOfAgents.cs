using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>Список пошукових агентів</returns>
        public static IEnumerable<ISearchAgent> GetAllAgents()
        {
            var listOfAgents = new List<ISearchAgent>
            {
                new ApiSearcherAgent(),
                new HtmlSearcherAgent()
            };

            return listOfAgents;
        }
    }
}
