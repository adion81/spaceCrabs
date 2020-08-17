using System;
using System.ComponentModel.DataAnnotations;

namespace SpaceCrabs.Validations
{
    public class EighteenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is DateTime)
            {
                DateTime check = (DateTime)value;
                if(DateTime.Now.AddYears(-18) > check)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Crab must be 18 years or older.");
                }
            }
            else
            {
                return new ValidationResult("Please enter a valid Date.");
            }
        }
    }
}