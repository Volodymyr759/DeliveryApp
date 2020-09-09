namespace Delivery.BLL.DTO
{
    /// <summary>
    /// User Dto model
    /// </summary>
    public class AppUserDto
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ІUser name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; }
    }
}
