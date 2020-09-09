using System;

namespace Delivery.BLL.DTO
{
    /// <summary>
    /// Dto model of user-tracked shipment.
    /// </summary>
    public class InvoiceDto
    {
        /// <summary>
        /// Shipment Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Postal operator name
        /// </summary>
        public string PostOperatorName { get; set; }

        /// <summary>
        /// Shipment number in the information system of one of the postal operators
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Date of sending according to the information system of the postal operator.
        /// </summary>
        public DateTime SendDateTime { get; set; }

        /// <summary>
        /// Sender name
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Sender address
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        /// Recipient name
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Recipient address
        /// </summary>
        public string RecipientAddress { get; set; }

        /// <summary>
        /// The address of the current location of shipment
        /// </summary>
        public string CurrentLocation { get; set; }

        /// <summary>
        /// Shipment current status
        /// </summary>
        public string ActualStatus { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
    }
}
