using Weather.Domain.Entities;

namespace Weather.Domain.Services.YrNo
{
    /// <summary>
    /// Interface for YrNoWebService
    /// </summary>
    public interface IYrNoWebService
    {
        Place GetPlaceForecast(string region, string place);
    }
}