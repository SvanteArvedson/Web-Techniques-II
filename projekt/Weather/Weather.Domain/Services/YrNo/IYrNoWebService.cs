using Weather.Domain.Entities;
namespace Weather.Domain.Services.YrNo
{
    public interface IYrNoWebService
    {
        Place GetPlaceForecast(string region, string place);
    }
}