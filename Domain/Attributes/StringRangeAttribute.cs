using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EquipmentControll.Domain.Attributes
{
    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowableValues?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            var msg = $"Field {validationContext.DisplayName} supports only this values: " +
                $"{string.Join(", ", (AllowableValues ?? new string[] { "No allowable values found" }))}.";
            return new ValidationResult(msg);
        }
    }
}
