using Delivery.BLL.DTO;
using Delivery.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Сервіс адміністратора
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Створення нового користувача
        /// </summary>
        /// <param name="userDto">Модель передачі даних зі сторінки реєстрації</param>
        /// <returns>Повідомлення про результат виконання операції створення користувача</returns>
        Task AddUser(AppUserDto userDto);

        /// <summary>
        /// Аутентифікація користувача
        /// </summary>
        /// <param name="userDto">Модель передачі даних зі сторінки логування</param>
        /// <returns>Об'єкт ClaimsIdentity</returns>
        Task<ClaimsIdentity> Authenticate(AppUserDto userDto);

        /// <summary>
        /// Повертає список усіх користувачів
        /// </summary>
        /// <returns>Список користувачів</returns>
        IEnumerable<AppUserDto> GetUsers();

        /// <summary>
        /// Повертає користувача по ідентифікатору
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Dto екземпляр користувача</returns>
        AppUserDto GetUserById(string userId);

        /// <summary>
        /// Видаляє користувача з каскадним видаленням пов'язаних даних
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Task</returns>
        Task RemoveUser(string userId);

        /// <summary>
        /// Збереження змін БД
        /// </summary>
        /// <returns>Task</returns>
        Task SaveAsync();

    }
}
