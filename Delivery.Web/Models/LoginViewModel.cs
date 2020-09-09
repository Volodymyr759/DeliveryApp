using System.ComponentModel.DataAnnotations;

namespace Delivery.Web.Models
{
    /// <summary>
    /// Login ViewModel
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Shows whether to save local log data
        /// </summary>
        [Display(Name = "Запам'ятати мене?")]
        public bool RememberMe { get; set; }
    }
}