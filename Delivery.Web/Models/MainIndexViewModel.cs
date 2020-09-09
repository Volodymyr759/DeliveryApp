using Delivery.BLL.DTO;
using System.Collections.Generic;

namespace Delivery.Web.Models
{
    /// <summary>
    /// Main page ViewModel
    /// </summary>
    public class MainIndexViewModel
    {
        /// <summary>
        /// The number of the shipment, that unregistered users can enter to search
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// List of postal operators implemented in the Delivery system, in the information systems of which the search is performed
        /// </summary>
        public IEnumerable<PostOperatorDto> PostOperators { get; set; }
    }
}