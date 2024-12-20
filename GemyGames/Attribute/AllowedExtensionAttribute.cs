using GemyGames.Settings;
using System.ComponentModel.DataAnnotations;

namespace GemyGames.Attribute
{
    public class AllowedExtensionAttribute : ValidationAttribute
    {

        private readonly string _allowedExtension;

        public AllowedExtensionAttribute(string allowedExtension)
        {
                _allowedExtension = allowedExtension;
        }


        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as FormFile;

            if (file != null)
            {

                var Extension=Path.GetExtension(file.FileName);

                var valid=_allowedExtension.Split(',')
                    .Contains(Extension,StringComparer.OrdinalIgnoreCase);

                if (!valid)
                {
                    return new ValidationResult($"Only {_allowedExtension} is vaild");
                }

            }

            return ValidationResult.Success;
        }


    }
}
