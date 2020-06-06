namespace Delivery.BLL.DTO
{
    /// <summary>
    /// Dto модель ролі користувача
    /// </summary>
    public class AppRoleDto
    {
        /// <summary>
        /// Ідентифікатор користувача, створений при реєстрації ASP.NET Identity
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Ідентифікатор ролі користувача ASP.NET Identity
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Назва ролі користувача
        /// </summary>
        public string RoleName { get; set; }
    }
}
