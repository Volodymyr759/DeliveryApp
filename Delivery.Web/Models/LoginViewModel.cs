using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Delivery.Web.Models
{
    /// <summary>
    /// ViewModel логування користувача
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email користувача
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Пароль користувача
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Показує чи зберігати логально дані логування
        /// </summary>
        [Display(Name = "Запам'ятати мене?")]
        public bool RememberMe { get; set; }
    }
}