using FluentValidation.Results;
using System;

namespace Delivery.BLL.Validators
{
    /// <summary>
    /// Auxiliary class for validation results processing
    /// </summary>
    public class ValidationResultsHelper
    {
        /// <summary>
        /// Returns a line with a set of validation errors
        /// </summary>
        /// <param name="results">ValidationResult - the result of validation of the entity instance</param>
        /// <returns>Line with a set of validation errors</returns>
        public static string GetValidationErrors(ValidationResult results)
        {
            string errors = "";
            foreach (ValidationFailure failure in results.Errors)
                errors += $"{ failure.ErrorMessage } " + Environment.NewLine;
            return errors;
        }
    }
}
