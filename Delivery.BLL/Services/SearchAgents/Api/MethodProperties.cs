namespace Delivery.BLL.Services
{
    /// <summary>
    /// Request settings class in the New Mail API service
    /// </summary>
    public class ApiMethodProperties
    {
        /// <summary>
        /// Start date of the requested period
        /// </summary>
        public string DateTimeFrom { get; set; }

        /// <summary>
        /// End date of the requested period
        /// </summary>
        public string DateTimeTo { get; set; }

        /// <summary>
        /// A parameter that indicates the need to obtain a complete list of invoices
        /// </summary>
        public string GetFullList { get; set; } 
    }
}
