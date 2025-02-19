using System.ComponentModel.DataAnnotations;

namespace BookHandling.Validation
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.UtcNow)
                {
                    return new ValidationResult(ErrorMessage ?? "Datumet måste vara idag eller tidigare.");
                }
            }
            return ValidationResult.Success;
        }
    }
}