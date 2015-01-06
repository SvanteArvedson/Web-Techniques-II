using System.Collections.Generic;
using Weather.Domain.Entities;

namespace Weather.Domain.Services.GeoNames
{
    public interface IGeoNamesWebService
    {
        IEnumerable<Place> SerachPlace(string search);
    }
}
