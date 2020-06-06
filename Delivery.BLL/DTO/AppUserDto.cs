﻿namespace Delivery.BLL.DTO
{
    /// <summary>
    /// Dto модель користувача
    /// </summary>
    public class AppUserDto
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
