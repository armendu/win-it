using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Presentation.Helpers
{
    public class BetNumberAttribute : ValidationAttribute, IClientModelValidator
    {
        private int _year;

        public BetNumberAttribute(int year)
        {
            _year = year;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            throw new System.NotImplementedException();
        }

        private string GetErrorMessage()
        {
            return $"Classic movies must have a release year earlier than {_year}.";
        }
    }
}