using FluentValidation.Results;
using System;

namespace Delivery.BLL.Validators
{
    /// <summary>
    /// Допоміжний клас обробки результатів валідації
    /// </summary>
    public class ValidationResultsHelper
    {
        /// <summary>
        /// Повертає строку з набором помилок валідації
        /// </summary>
        /// <param name="results">ValidationResult - результат валідації екземпляру сутності</param>
        /// <returns>Строка,, конкантинуюча список помилок валідації</returns>
        public static string GetValidationErrors(ValidationResult results)
        {
            string errors = "";
            foreach (ValidationFailure failure in results.Errors)
                errors += $"{ failure.ErrorMessage } " + Environment.NewLine;
            return errors;
        }
    }
}
