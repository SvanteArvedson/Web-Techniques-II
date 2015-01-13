using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Weather.Domain.Validation
{
    /// <summary>
    /// Interface for DataAnnotationValidator
    /// </summary>
    public interface IDataAnnotationValidator
    {
        bool TryValidate(object @object, out ICollection<ValidationResult> results);
    }
}
