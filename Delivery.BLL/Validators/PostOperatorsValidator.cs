using Delivery.DAL.Models;
using FluentValidation;

namespace Delivery.BLL.Validators
{
    /// <summary>
    /// Валідатор поштового оператора
    /// </summary>
    public class PostOperatorsValidator : AbstractValidator<IPostOperator>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PostOperatorsValidator()
        {
            RuleFor(po => po.Name)
                .NotEmpty().WithMessage("Введіть назву від 1 до 30 символів.")
                .Length(1, 30).WithMessage("Довжина назви від 1 до 30 символів.")
                .Must(BeValidName);
            RuleFor(po => po.LinkToSearchPage)
                .NotEmpty().WithMessage("Введіть адресу пошукової сторінки від 7 до 300 символів.")
                .Length(7, 300).WithMessage("Довжина адреси пошукової сторінки від 7 до 300 символів.");
            RuleFor(po => po.PathToLogoImage)
                .NotEmpty().WithMessage("Введіть шлях до файлу від 2 до 300 символів.")
                .Length(4, 300).WithMessage("Довжина шляху до файлу від 2 до 300 символів.");
            RuleFor(po => po.IsActive)
                .NotNull().WithMessage("Не вказано активацію оператора.");
            RuleFor(i => i.Notes)
                .Length(0, 300).WithMessage("Довжина приміток від 0 до 300 символів."); ;
        }

        private bool BeValidName(string arg)
        {
            return arg.Contains("Нова Пошта")||arg.Contains("Укрпошта");
        }
    }
}
