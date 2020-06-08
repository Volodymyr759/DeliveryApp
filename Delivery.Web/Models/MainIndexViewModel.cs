using Delivery.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivery.Web.Models
{
    /// <summary>
    /// ViewModel головної сторінки
    /// </summary>
    public class MainIndexViewModel
    {
        /// <summary>
        /// Номер відправлення, який можуть вводити для пошуку незареєстровані користувачі
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Список реалізованих в системі Delivery поштових операторів, в інформаційних системах яких здійснюється пошук
        /// </summary>
        public IEnumerable<PostOperatorDto> PostOperators { get; set; }
    }
}