using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Weather.Domain.Validation
{
    /// <summary>
    /// Class for validating database objects before saving them in database.
    /// Validates with DataAnnotations.
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