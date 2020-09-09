using System.Collections.Generic;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// A class for creating search agents by the factory method
    /// </summary>
    public class FactoryOfAgents
    {
        /// <summary>
        /// Returns a list of search agents implemented in the Delivery system
        /// </summary>
        /// <param name="apiKeys">Access keys to implemented Api-services</param>
        /// <returns>List of search agents</returns>
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
