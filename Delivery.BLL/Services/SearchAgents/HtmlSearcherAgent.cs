using System;
using System.Threading.Tasks;
using Delivery.BLL.DTO;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Html-parser of the postal operator
    /// </summary>
    public class HtmlSearcherAgent : ISearchAgent
    {
        private readonly string name = "Укрпошта";

        /// <summary>
        /// Returns the name of the search agent implemented in the Delivery system
        /// </summary>
        /// <returns>Search agent name</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Searches for a postal item by number and returns the current status from the information system of the postal operator
        /// </summary>
        /// <param name="number">Shipment number</param>
        /// <returns>Current shipment status</returns>
        public Task<string> GetStatus(string number)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches for a postal item by number and returns the Dto model, including the name of the operator
        /// </summary>
        /// <param name="number">Shipment number in the information system of the postal operator</param>
        /// <returns>An instance of the Dto shipment model</returns>
        public Task<InvoiceDto> SearchByNumber(string number)
        {
            throw new NotImplementedException();
        }
    }
}
