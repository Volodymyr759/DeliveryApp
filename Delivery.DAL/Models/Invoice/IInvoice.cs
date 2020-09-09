using System;

namespace Delivery.DAL.Models
{
    /// <summary>
    /// User-tracked shipment's interface. It is assumed that it exists in the information system of the postal operator
    /// </summary>
    public interface IInvoice
    {
        /// <summary>
        /// Shipment Id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        string AccountUserId { get; set; }

        /// <summary>
        /// Postal operator Id
        /// </summary>
        int PostOperatorId { get; set; }

        /// <summary>
        /// Shipment number in the information system of one of the postal operators
        /// </summary>
        string Number { get; set; }

        /// <summary>
        /// Date of sending according to the information system of the postal operator
        /// </summary>
        DateTime SendDateTime { get; set; }

        /// <summary>
        /// Sender name
        /// </summary>
        string Sender { get; set; }

        /// <summary>
        /// Sender address
        /// </summary>
        string SenderAddress { get; set; }

        /// <summary>
        /// Recipient name
        /// </summary>
        string Recipient { get; set; }

        /// <summary>
        /// Recipient address
        /// </summary>
        string RecipientAddress { get; set; }

        /// <summary>
        /// The address of the current location of shipment
        /// </summary>
        string CurrentLocation { get; set; }

        /// <summary>
        /// Shipment current status
        /// </summary>
        string ActualStatus { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        string Notes { get; set; }
    }
}