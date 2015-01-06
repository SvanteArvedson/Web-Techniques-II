using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Weather.Domain.Validation
{
    /// <summary>
    /// Class for validating objects before saving them in database
    /// </summary>
    public class DataAnnotationValidator :IDataAnnotationValidator
    {
        public bool TryValidate(object @object, out ICollection<ValidationResult> results)
        {
            ValidationContext validationContext = new ValidationContext(@object);
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(@object, validationContext, results, true);
        }
    }
}