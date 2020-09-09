using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.BLL.DTO;
using Delivery.DAL.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Admin service
    /// </summary>
    public class AdminService : IAdminService
    {
        private UserManager<IdentityUser> userManager;

        private ApplicationDbContext db;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public AdminService(string connectionString)
        {
            db = new ApplicationDbContext(connectionString);
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(db));
        }

        /// <summary>
        /// Creating a new user
        /// </summary>
        /// <param name="userDto">Data transfer object from the registration page</param>
        /// <returns>Notification of the result of creating a user</returns>
        public async Task AddUser(AppUserDto userDto)
        {
            IdentityUser user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new IdentityUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0) throw new Exception(result.Errors.FirstOrDefault());
                await SaveAsync();
            }
            else
            {
                throw new Exception("Користувач з таким логіном вже існує");
            }
        }

        /// <summary>
        /// User authentication
        /// </summary>
        /// <param name="userDto">Data transfer object from the login page</param>
        /// <returns>Об'єкт ClaimsIdentity</returns>
        public async Task<ClaimsIdentity> Authenticate(AppUserDto userDto)
        {
            ClaimsIdentity claim = null;
            // Знаходимо користувача
            IdentityUser user = await userManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуємо його і повертаємо ClaimsIdentity
            if (user != null) claim = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        /// <summary>
        /// Returns the user by ID
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Dto instance of the user</returns>
        public AppUserDto GetUserById(string userId)
        {
            var mapper = new MapperConfiguration(cgf => cgf.CreateMap<IdentityUser, AppUserDto>()).CreateMapper();

            return mapper.Map<AppUserDto>(userManager.FindById(userId));
        }

        /// <summary>
        /// Returns a list of all users
        /// </summary>
        /// <returns>List of users</returns>
        public IEnumerable<AppUserDto> GetUsers()
        {
            var mapper = new MapperConfiguration(cgf => cgf.CreateMap<IdentityUser, AppUserDto>()).CreateMapper();

            return mapper.Map<List<AppUserDto>>(userManager.Users.ToList().OrderBy(u => u.UserName));
        }

        /// <summary>
        /// Deletes the user by cascading the associated data
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Task</returns>
        public async Task RemoveUser(string userId)
        {
            await userManager.DeleteAsync(userManager.FindById(userId));
        }

        /// <summary>
        /// Saving database changes
        /// </summary>
        /// <returns>Task</returns>
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
