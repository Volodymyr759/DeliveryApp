﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.DAL.Models
{
    /// <summary>
    /// Поштовий оператор
    /// </summary>
    public class PostOperator : IPostOperator
    {
        /// <summary>
        /// Ідентифікатор поштового оператору в базі даних сервісу Delivery
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Назва поштового оператора, додається лише після реалізації відповідного пошукового агента
        /// з одночасним додаванням у базу даних
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пошукова сторінка для відстеження відправлень за номером в інформаційній системі поштового оператора.
        /// </summary>
        public string LinkToSearchPage { get; set; }

        /// <summary>
        /// Шлях до розташування зображення лого поштового оператора на хост-сервері сервісу Delivery
        /// </summary>
        public string PathToLogoImage { get; set; }

        /// <summary>
        /// Показує, чи оператор доступний для використання. Адміністратор може деактивовувати оператора при змінах умов доступу,
        /// які ще не реалізовані в системі Delivery.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Примітки
        /// </summary>
        public string Notes { get; set; }
    }
}