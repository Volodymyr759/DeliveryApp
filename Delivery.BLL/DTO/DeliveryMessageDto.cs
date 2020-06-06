﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.BLL.DTO
{
    /// <summary>
    /// Клас повідомлень користувача у системі Delivery
    /// </summary>
    public class DeliveryMessageDto
    {
        /// <summary>
        /// Заголовок повідомлення
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Тіло повідомлення
        /// </summary>
        public string Body { get; set; }
    }
}
