using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace OngProject.Core.Helper
{
   
        public class ExtensionsValidationHelper : ValidationAttribute
        {
            private readonly string[] _extensions;

            public ExtensionsValidationHelper(string[] extensions)
            {
                _extensions = extensions;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
               if(value == null)
            {
                return ValidationResult.Success;
            }
                var file = value as IFormFile;
                var extension = Path.GetExtension(file.FileName);
                if (file != null)
                {
                    if (!_extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
                return ValidationResult.Success;
            }

            public string GetErrorMessage()
            {
                return $"Your image's filetype is not valid.";
            }
        }
    
}
