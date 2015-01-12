using System.Collections.Generic;
using Weather.Domain.Entities;

namespace Weather.Domain
{
    public interface IWeatherService
    {
        IEnumerable<Place> SearchPlace(string search);
        Place GetWeatherForecast(string region, string name);
    }
}