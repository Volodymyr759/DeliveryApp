using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.BLL.DTO;
using Delivery.DAL.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Delivery.BLL.Services
{
    /// <summary>
    /// Сервіс адміністратора
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly string connectionString;

        private UserManager<IdentityUser> userManager;

        private ApplicationDbContext db;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public AdminService(string connectionString)
        {
            this.connectionString = connectionString;
            db = new ApplicationDbContext(connectionString);
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(db));
        }

        /// <summary>
        /// Створення нового користувача
        /// </summary>
        /// <param name="userDto">Модель передачі даних зі сторінки реєстрації</param>
        /// <returns>Повідомлення про результат виконання операції створення користувача</returns>
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
        /// Аутентифікація користувача
        /// </summary>
        /// <param name="userDto">Модель передачі даних зі сторінки логування</param>
        /// <returns>Об'єкт ClaimsIdentity</returns>
        public async Task<ClaimsIdentity> Authenticate(AppUserDto userDto)
        {
            ClaimsIdentity claim = null;
            // Знаходимо користувача
            IdentityUser user = await userManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуємо його і повертаєм об'ект ClaimsIdentity
            if (user != null)
                claim = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        /// <summary>
        /// Повертає користувача по ідентифікатору
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Dto екземпляр користувача</returns>
        public AppUserDto GetUserById(string userId)
        {
            var mapper = new MapperConfiguration(cgf => cgf.CreateMap<IdentityUser, AppUserDto>()).CreateMapper();

            return mapper.Map<AppUserDto>(userManager.FindById(userId));
        }

        /// <summary>
        /// Повертає список усіх користувачів
        /// </summary>
        /// <returns>Список користувачів</returns>
        public IEnumerable<AppUserDto> GetUsers()
        {
            var mapper = new MapperConfiguration(cgf => cgf.CreateMap<List<IdentityUser>, List<AppUserDto>>()).CreateMapper();

            return mapper.Map<List<AppUserDto>>(userManager.Users.ToList().OrderBy(u => u.UserName));
        }

        /// <summary>
        /// Видаляє користувача з каскадним видаленням пов'язаних даних
        /// </summary>
        /// <param name="userId">Ідентифікатор користувача</param>
        /// <returns>Task</returns>
        public async Task RemoveUser(string userId)
        {
            await userManager.DeleteAsync(userManager.FindById(userId));
        }

        /// <summary>
        /// Збереження змін БД
        /// </summary>
        /// <returns>Task</returns>
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
