using System.Drawing;

namespace GameZone.Validation
{
    public class ValidationSize : ValidationAttribute
    {
        private readonly int _size;

        public ValidationSize(int size)
        {
            _size = size;

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var Iform = value as IFormFile;
            if (Iform != null)
            {
                if(Iform.Length > _size)
                {
                    return new ValidationResult("More Than 1Mb");
                }


            }
            return ValidationResult.Success;
        }

    }
}
