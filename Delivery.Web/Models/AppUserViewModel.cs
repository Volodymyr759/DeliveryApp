namespace Delivery.Web.Models
{
    /// <summary>
    /// User ViewModel
    /// </summary>
    public class AppUserViewModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User name
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