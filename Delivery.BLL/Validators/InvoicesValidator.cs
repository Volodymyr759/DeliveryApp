using Delivery.DAL.Models;
using FluentValidation;
using System;

namespace Delivery.BLL.Validators
{
    /// <summary>
    /// Shipment validator
    /// </summary>
    public class InvoicesValidator : AbstractValidator<IInvoice>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public InvoicesValidator()
        {
            RuleFor(i => i.AccountUserId)
                .NotEmpty().WithMessage("Оберіть користувача.");
            RuleFor(i => i.PostOperatorId)
                .NotEmpty().WithMessage("Оберіть поштового оператора.")
                .GreaterThan(0).WithMessage("Не обрано поштового оператора.");
            RuleFor(i => i.Number)
                .NotEmpty().WithMessage("Введіть номер від 6 до 30 символів.")
                .Length(6, 30).WithMessage("Довжина номеру від 6 до 30 символів.");
            RuleFor(i => i.SendDateTime)
                .Must(BeAValidDate).WithMessage("Дата відправлення не раніше 1 міс. від сьогодні і не пізніше 100 р. від сьогодні");
            RuleFor(i=>i.Sender)
                .Length(0, 128).WithMessage("Довжина назви відправника від 0 до 128 символів.");
            RuleFor(i => i.SenderAddress)
                .Length(0, 300).WithMessage("Довжина адреси відправника від 0 до 300 символів.");
            RuleFor(i => i.Recipient)
                .Length(0, 128).WithMessage("Довжина назви одержувача від 0 до 128 символів.");
            RuleFor(i => i.RecipientAddress)
                .Length(0, 300).WithMessage("Довжина адреси одержувача від 0 до 300 символів.");
            RuleFor(i => i.CurrentLocation)
                .Length(0, 300).WithMessage("Довжина адреси місцезнаходження від 0 до 300 символів.");
            RuleFor(i => i.CurrentLocation)
                .Length(0, 300).WithMessage("Довжина актуального статусу відправлення від 0 до 300 символів.");
            RuleFor(i => i.Notes)
                .Length(0, 300).WithMessage("Довжина приміток від 0 до 300 символів.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return (date >= DateTime.Today.AddMonths(-1) & date <= DateTime.Today.AddYears(100));
        }
    }
}
