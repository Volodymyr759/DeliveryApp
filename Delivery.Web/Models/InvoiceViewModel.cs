using System;

namespace Delivery.Web.Models
{
    /// <summary>
    /// Shipment ViewModel
    /// </summary>
    public class InvoiceViewModel
    {
        /// <summary>
        /// Shipment Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Shipment number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Postal operator name
        /// </summary>
        public string PostOperatorName { get; set; }

        /// <summary>
        /// Date of sending according to the information system of the postal operator
        /// </summary>
        public DateTime SendDateTime { get; set; }

        /// <summary>
        /// The address of the current location of shipment
        /// </summary>
        public string CurrentLocation { get; set; }

        /// <summary>
        /// Current shipment status
        /// </summary>
        public string ActualStatus { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
    }
}