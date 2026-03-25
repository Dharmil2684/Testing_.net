using System.ComponentModel.DataAnnotations;

namespace Testing_.net.Validators
{
    public class DateCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Date is required");

            var date = (DateTime)value;

            if (date < DateTime.Now)
            {
                return new ValidationResult("Date must be today or in the future");
            }

            return ValidationResult.Success;
        }
    }
}