using System.ComponentModel.DataAnnotations;

namespace Cinema.Extensions
{
    public class PastDateValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime || value is DateTime?)
            {
                var date = (DateTime)value;
                if (date <= DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("The given date is in the future"); //todo: fix message
            }
            return new ValidationResult("The object is not of type DateTime or DateTime?.");
        }
    }
}
