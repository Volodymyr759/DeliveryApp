using Delivery.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Shipment Management Service interface
    /// </summary>
    public interface IInvoicesService
    {

        /// <summary>
        /// Create a new shipment
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="number">Shipment number</param>
        Task Add(string userId, string number);

        /// <summary>
        /// Returns all user-generated shipments in the Delivery service
        /// </summary>
        /// <returns>List of shipments</returns>
        IEnumerable<InvoiceDto> GetAll();

        /// <summary>
        /// Returns the shipment by Id
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        /// <returns>Shipment Dto model</returns>
        InvoiceDto GetById(int invoiceId);

        /// <summary>
        /// Returns the shipment list of the selected user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        IEnumerable<InvoiceDto> GetInvoicesByUserId(string userId);

        /// <summary>
        /// Deleting a shipment
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        void Remove(int invoiceId);

        /// <summary>
        /// Delete user shipments
        /// </summary>
        /// <param name="userId">User Id</param>
        void RemoveByUser(string userId);

        /// <summary>
        /// Search for a shipment by number
        /// </summary>
        /// <param name="number">Shipment number in the information system of the postal operator</param>
        /// <returns>Shipment Dto model</returns>
        Task<InvoiceDto> SearchByNumber(string number);

        /// <summary>
        /// Updates the status of the shipment
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        Task UpdateStatusAsync(int invoiceId);
    }
}
