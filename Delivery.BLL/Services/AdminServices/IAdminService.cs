using Delivery.BLL.DTO;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Admin service interface
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Creating a new user
        /// </summary>
        /// <param name="userDto">Data transfer object from the registration page</param>
        /// <returns>Notification of the result of creating a user</returns>
        Task AddUser(AppUserDto userDto);

        /// <summary>
        /// User authentication
        /// </summary>
        /// <param name="userDto">Data transfer object from the login page</param>
        /// <returns>Об'єкт ClaimsIdentity</returns>
        Task<ClaimsIdentity> Authenticate(AppUserDto userDto);

        /// <summary>
        /// Returns a list of all users
        /// </summary>
        /// <returns>List of users</returns>
        IEnumerable<AppUserDto> GetUsers();

        /// <summary>
        /// Returns the user by ID
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Dto instance of the user</returns>
        AppUserDto GetUserById(string userId);

        /// <summary>
        /// Deletes the user by cascading the associated data
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Task</returns>
        Task RemoveUser(string userId);

        /// <summary>
        /// Saving database changes
        /// </summary>
        /// <returns>Task</returns>
        Task SaveAsync();

    }
}
