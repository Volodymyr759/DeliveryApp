using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Delivery.Web.Models
{
    /// <summary>
    /// ViewModel поштового оператора
    /// </summary>
    public class PostOperatorViewModel
    {
        /// <summary>
        /// Ідентифікатор поштового оператору в базі даних сервісу Delivery
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Назва поштового оператора
        /// </summary>
        [Required]
        [Display(Name ="Назва поштового оператора")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Введіть назву від 1 до 30 символів.")]
        public string Name { get; set; }

        /// <summary>
        /// Пошукова сторінка для відстеження відправлень за номером в інформаційній системі поштового оператора.
        /// </summary>
        [Required]
        [Display(Name = "Посилання на сторінку відстеження відправлень")]
        [StringLength(300, MinimumLength = 7, ErrorMessage = "Введіть посилання від 7 до 300 символів.")]
        public string LinkToSearchPage { get; set; }

        /// <summary>
        /// Шлях до розташування зображення лого поштового оператора на хост-сервері сервісу Delivery
        /// </summary>
        [Required]
        [Display(Name = "Шлях до розташування зображення лого")]
        [StringLength(300, MinimumLength = 2, ErrorMessage = "Введіть шлях від 2 до 300 символів.")]
        public string PathToLogoImage { get; set; }

        /// <summary>
        /// Показує, чи оператор доступний для використання. Адміністратор може деактивовувати оператора при змінах умов доступу,
        /// які ще не реалізовані в системі Delivery.
        /// </summary>
        [Required]
        [Display(Name = "Оператор активний?")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Примітки
        /// </summary>
        [Display(Name = "Опис")]
        [StringLength(300, MinimumLength = 0, ErrorMessage = "Необов'язкове поле опису, від 0 до 300 символів.")]
        public string Notes { get; set; }
    }
}