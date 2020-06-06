using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Delivery.Web.Models
{
    /// <summary>
    /// ViewModel пошуку і створення відправлення по номеру інформаційної системи поштового оператора
    /// </summary>
    public class SearchInvoiceViewModel
    {
        /// <summary>
        /// Номер відправлення в інформаційній системі поштового оператора
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Введіть номер від 6 до 30 символів.")]
        public string Number { get; set; }
    }
}