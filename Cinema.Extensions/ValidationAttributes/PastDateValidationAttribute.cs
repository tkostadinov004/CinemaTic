using System.ComponentModel.DataAnnotations;

namespace Cinema.Extensions
{
    public class PastDateValidationAttribute : ValidationAttribute
    {
        public PastDateValidationAttribute()
        {
            
        }
        public override bool IsValid(object? value)
        {
            if (value is DateTime || value is DateTime?)
            {
                var date = (DateTime)value;
                if (date <= DateTime.Now)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime || value is DateTime?)
            {
                var date = (DateTime)value;
                if (date <= DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(this.ErrorMessageString);
            }
            return new ValidationResult("The entered data is not a date!");
        }
    }
}
