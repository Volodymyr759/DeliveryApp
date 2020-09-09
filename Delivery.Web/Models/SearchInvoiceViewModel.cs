using System.ComponentModel.DataAnnotations;

namespace Delivery.Web.Models
{
    /// <summary>
    /// View model for search and create a shipment by the number of the information system of the postal operator
    /// </summary>
    public class SearchInvoiceViewModel
    {
        /// <summary>
        /// Sipment number in the information system of the postal operator
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Введіть номер від 6 до 30 символів.")]
        public string Number { get; set; }
    }
}