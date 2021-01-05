using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        public string GetErrorMessage() =>
            $"Maximum file size is {maxFileSize} bytes.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > maxFileSize)
                    return new ValidationResult(ErrorMessage ?? GetErrorMessage());

                return ValidationResult.Success;
            }
            if (!Attribute.IsDefined(validationContext.ObjectType
                .GetProperty(validationContext.MemberName), typeof(RequiredAttribute)))
                return ValidationResult.Success;

            return new ValidationResult("Invalid file.");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val"))
                context.Attributes.Add("data-val", "true");

            context.Attributes.Add("data-val-max-file-size", ErrorMessage ?? GetErrorMessage());
            context.Attributes.Add("data-val-max-file-size-value", maxFileSize.ToString());
        }
    }
}
