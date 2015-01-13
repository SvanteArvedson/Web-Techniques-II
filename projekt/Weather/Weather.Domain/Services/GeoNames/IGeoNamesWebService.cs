using System.Collections.Generic;
using Weather.Domain.Entities;

namespace Weather.Domain.Services.GeoNames
{
    /// <summary>
    /// Interface for GeoNameWebService
    /// </summary>
    public interface IGeoNamesWebService
    {
        IEnumerable<Place> SerachPlace(string search);
    }
}
