using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivery.Web.Models
{
    /// <summary>
    /// ViewModel користувача
    /// </summary>
    public class AppUserViewModel
    {
        /// <summary>
        /// Ідентифікатор користувача
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Ім'я користувача
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email користувача
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль користувача
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль користувача
        /// </summary>
        public string Role { get; set; }
    }
}