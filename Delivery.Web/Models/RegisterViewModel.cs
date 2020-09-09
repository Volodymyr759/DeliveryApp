using System.ComponentModel.DataAnnotations;

namespace Delivery.Web.Models
{
    /// <summary>
    /// ViewModel user registration
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Пароль повинен містити не менше 6 символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Confirm user password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Пароль і підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }

    }
}