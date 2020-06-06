using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
