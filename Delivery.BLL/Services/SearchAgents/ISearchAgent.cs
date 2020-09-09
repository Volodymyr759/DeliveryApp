using Delivery.BLL.DTO;
using System.Threading.Tasks;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Search agents interface
    /// </summary>
    public interface ISearchAgent
    {
        /// <summary>
        /// Returns the name of the search agent implemented in the Delivery system
        /// </summary>
        /// <returns>Search agent name</returns>
        string GetName();

        /// <summary>
        /// Searches for a postal item by number and returns the current status from the information system of the postal operator
        /// </summary>
        /// <param name="number">Shipment number</param>
        /// <returns>Current shipment status</returns>
        Task<string> GetStatus(string number);

        /// <summary>
        /// Searches for a postal item by number and returns the Dto model, including the name of the operator
        /// </summary>
        /// <param name="number">Shipment number in the information system of the postal operator</param>
        /// <returns>An instance of the Dto shipment model</returns>
        Task<InvoiceDto> SearchByNumber(string number);
    }
}
