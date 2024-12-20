using GemyGames.Settings;
using System.ComponentModel.DataAnnotations;

namespace GemyGames.Attribute
{
    public class MaxFileSizeAttribute: ValidationAttribute
    {
        private readonly int _maxSizeFile;

        public MaxFileSizeAttribute(int maxSizeFile)
        {
            _maxSizeFile = maxSizeFile;
        }


        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as FormFile;

            if (file != null)
            {

                

                var valid = file.Length<=ImageSettings.MaxSizeFileInBytes;
                if (!valid)
                {
                    return new ValidationResult($"Maximum Allowed Size Vaild Is {ImageSettings.MaxSizeFileInBytes} Bytes");
                }

            }

            return ValidationResult.Success;
        }

    }
}
