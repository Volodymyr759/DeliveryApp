using System.ComponentModel.DataAnnotations;

namespace Delivery.Web.Models
{
    /// <summary>
    /// PostOperator ViewModel
    /// </summary>
    public class PostOperatorViewModel
    {
        /// <summary>
        /// PostOperator Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// PostOperator name
        /// </summary>
        [Required]
        [Display(Name ="Назва поштового оператора")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Введіть назву від 1 до 30 символів.")]
        public string Name { get; set; }

        /// <summary>
        /// Search page for tracking shipments by number in the information system of the postal operator.
        /// </summary>
        [Required]
        [Display(Name = "Посилання на сторінку відстеження відправлень")]
        [StringLength(300, MinimumLength = 7, ErrorMessage = "Введіть посилання від 7 до 300 символів.")]
        public string LinkToSearchPage { get; set; }

        /// <summary>
        /// The path to the location of the image of the postal operator's logo on the host server of the Delivery service
        /// </summary>
        [Required]
        [Display(Name = "Шлях до розташування зображення лого")]
        [StringLength(300, MinimumLength = 2, ErrorMessage = "Введіть шлях від 2 до 300 символів.")]
        public string PathToLogoImage { get; set; }

        /// <summary>
        /// Indicates whether the operator is available for use. The administrator can deactivate the operator in case of changes in access conditions that have not yet been implemented in the Delivery system.
        /// </summary>
        [Required]
        [Display(Name = "Оператор активний?")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [Display(Name = "Опис")]
        [StringLength(300, MinimumLength = 0, ErrorMessage = "Необов'язкове поле опису, від 0 до 300 символів.")]
        public string Notes { get; set; }
    }
}