namespace Delivery.BLL.Services
{
    /// <summary>
    /// Shipment class adapted to the json response format of the Nova Poshta API service
    /// </summary>
    public class ApiInvoicesModel
    {
        /// <summary>
        /// Shipment number
        /// </summary>
        public string IntDocNumber { get; set; }

        /// <summary>
        /// Recipient's office
        /// </summary>
        public string RecipientAddressDescription { get; set; }

        /// <summary>
        /// Recipient's city
        /// </summary>
        public string CityRecipientDescription { get; set; }

        /// <summary>
        /// Date the shipment was created
        /// </summary>
        public string Datetime { get; set; }

        /// <summary>
        /// Declared shipping cost
        /// </summary>
        public string CostOnSite { get; set; }

        /// <summary>
        /// Delivery type
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of payment: cash, non-cash
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Payer: sender or recipient
        /// </summary>
        public string PayerType { get; set; }

        /// <summary>
        /// Status, for example: Order in processing
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// Sender's phone
        /// </summary>
        public string SendersPhone { get; set; }

        /// <summary>
        /// Sender's office
        /// </summary>
        public string SenderAddressDescription { get; set; }

        /// <summary>
        /// Sender's city
        /// </summary>
        public string CitySenderDescription { get; set; }
        
    }
}
