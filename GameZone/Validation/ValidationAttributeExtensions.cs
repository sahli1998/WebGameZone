namespace GameZone.Validation
{
    public class ValidationAttributeExtensions : ValidationAttribute
    {
        private readonly string _allowedExtensions;
        public ValidationAttributeExtensions(string allowedExtensions)
        {
            this._allowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var form = value as IFormFile;
            if (form != null)
            {
                var extesion = Path.GetExtension(form.FileName);
                var isValid = _allowedExtensions.Split(",").Contains(extesion, StringComparer.OrdinalIgnoreCase);
                if (!isValid)
                {
                    return new ValidationResult($"Only {_allowedExtensions} extesions is allowed");

                }
            }

            return ValidationResult.Success;
        }

    }
}
