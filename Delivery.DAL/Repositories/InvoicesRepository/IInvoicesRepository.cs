using System.Collections.Generic;
using Delivery.DAL.Models;

namespace Delivery.DAL.Repositories
{
    /// <summary>
    /// Shipments repo's interface
    /// </summary>
    public interface IInvoicesRepository
    {
        /// <summary>
        /// Creates a new shipment
        /// </summary>
        /// <param name="invoice">Instance of the shipment</param>
        void Create(IInvoice invoice);

        /// <summary>
        /// Returns the list of all shipments
        /// </summary>
        /// <returns>The list of shipments</returns>
        IEnumerable<IInvoice> GetAll();

        /// <summary>
        /// Returns shipment by Id
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        /// <returns>Instance of the shipment</returns>
        IInvoice GetById(int invoiceId);

        /// <summary>
        /// Returns user's shipments
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>The list of the user's shipments</returns>
        IEnumerable<IInvoice> GetByUserId(string userId);

        /// <summary>
        /// Returns dictionary: Id - Name of postal operators
        /// </summary>
        /// <returns></returns>
        Dictionary<int, string> GetPostOperatorsIdNames();

        /// <summary>
        /// Deletes the user's shipment
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        void Delete(int invoiceId);

        /// <summary>
        /// Deletes all shipments by user Id
        /// </summary>
        /// <param name="userId">User Id</param>
        void DeleteByUserId(string userId);

        /// <summary>
        /// Updates the current shipment status
        /// </summary>
        /// <param name="invoiceId">Shipment Id</param>
        /// <param name="actualStatus">Current status</param>
        void UpdateStatus(int invoiceId, string actualStatus);
    }
}