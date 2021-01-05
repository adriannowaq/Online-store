using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineStore.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowedContentTypesAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] allowedContentTypes;

        public AllowedContentTypesAttribute(string[] allowedContentTypes)
        {
            this.allowedContentTypes = allowedContentTypes;
        }

        public string GetErrorMessage() =>
            $"Allowed content type {string.Join(", ", allowedContentTypes)}";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (!allowedContentTypes.Contains(file.ContentType))
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
            
            context.Attributes.Add("data-val-allowed-file-extensions", ErrorMessage ?? GetErrorMessage());
            context.Attributes.Add("data-val-allowed-file-extensions-value", string.Join(",", allowedContentTypes));
        }
    }
}
