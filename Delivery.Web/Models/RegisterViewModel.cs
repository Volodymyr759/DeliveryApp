using System.ComponentModel.DataAnnotations;

namespace Delivery.Web.Models
{
    /// <summary>
    /// ViewModel реєстрації користувача
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Email користувача
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль користувача
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Пароль повинен містити не менше 6 символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Підтвердження паролю користувача
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Пароль і підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }

    }
}