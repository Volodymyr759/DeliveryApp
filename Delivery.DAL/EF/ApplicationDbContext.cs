using Microsoft.AspNet.Identity.EntityFramework;

namespace Delivery.DAL.EF
{
    /// <summary>
    /// EntityFramework DbContext для використання AspNet.Identity
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="conectionString"></param>
        public ApplicationDbContext(string conectionString) : base(conectionString)
        {
            
        }
    }
}
