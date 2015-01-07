using System;
using System.Collections.Generic;
using System.Linq;
using Weather.Domain.Entities;
using Weather.Domain.PersistentStorage;
using Weather.Domain.Services.GeoNames;
using Weather.Domain.Services.YrNo;

namespace Weather.Domain
{
    /// <summary>
    /// Facade class for class library Weather Domain
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private IUnitOfWork _unitOfWork;
        private IGeoNamesWebService _geoNameWebService;
        private IYrNoWebService _yrNoWebService;

        public WeatherService() 
            : this(new GeoNamesWebService(), new YrNoWebService(), UnitOfWork.getInstance())
        {
            // Empty!
        }

        public WeatherService(IGeoNamesWebService geoNameWebService, IYrNoWebService yrNoWebService, IUnitOfWork unitOfWork)
        {
            _geoNameWebService = geoNameWebService;
            _yrNoWebService = yrNoWebService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Returns a collection of places matching a search word
        /// </summary>
        /// <param name="search">search word</param>
        /// <returns>A collection of places</returns>
        public IEnumerable<Place> SearchPlace(string search)
        {
            Search searchObject = _unitOfWork.SearchRepository.Select(x => x.Word.ToLower() == search.ToLower()).FirstOrDefault();

            if (searchObject != null)
            {
                if (searchObject.NextUpdate < DateTime.Now)
                {
                    // Delete old place objects
                    foreach (Place place in searchObject.Places.ToList())
                    {
                        _unitOfWork.PlaceRepository.Delete(place.PlaceId);
                    }

                    // Add new Places and new NextUpdate
                    searchObject.Places = (ICollection<Place>)_geoNameWebService.SerachPlace(search);
                    searchObject.NextUpdate = DateTime.Now.AddDays(30);
                    _unitOfWork.SearchRepository.Update(searchObject);

                    // Save changes
                    _unitOfWork.Save();
                }
            }
            else
            {
                searchObject = new Search()
                {
                    Word = search,
                    Places = (ICollection<Place>)_geoNameWebService.SerachPlace(search),
                    NextUpdate = DateTime.Now.AddDays(30)
                };
                _unitOfWork.SearchRepository.Insert(searchObject);
                _unitOfWork.Save();
            }

            return searchObject.Places;
        }

        /// <summary>
        /// Returns a collection of forecasts for a place
        /// </summary>
        /// <param name="region">Region name for the place</param>
        /// <param name="name">Name of the place</param>
        /// <returns>A collection of forecasts</returns>
        public IEnumerable<Forecast> GetWeatherForecast(string region, string name)
        {
            Place placeObject = _unitOfWork.PlaceRepository.
                    Select(x => x.Country.ToLower() == "Sverige" &&
                        x.Region.ToLower() == region.ToLower() &&
                        x.Name.ToLower() == name.ToLower()).FirstOrDefault();

            if (placeObject != null)
            {
                if (placeObject.NextUpdate < DateTime.Now)
                {
                    // Delete old forecasts
                    foreach (Forecast forecast in placeObject.Forecasts.ToList())
                    {
                        _unitOfWork.ForecastRepository.Delete(forecast.ForecastId);
                    }

                    // Add new Forecasts and new NextUpdate
                    Place newInformation = _yrNoWebService.GetPlaceForecast(region, name);
                    placeObject.NextUpdate = newInformation.NextUpdate;
                    placeObject.Forecasts = newInformation.Forecasts;
                    _unitOfWork.PlaceRepository.Update(placeObject);

                    // Save changes
                    _unitOfWork.Save();
                }
            }
            else
            {
                placeObject = _yrNoWebService.GetPlaceForecast(region, name);
                if (placeObject != null)
                {
                    _unitOfWork.PlaceRepository.Insert(placeObject);
                    _unitOfWork.Save();
                }
            }

            if (placeObject != null)
            {
                return placeObject.Forecasts;
            }
            else
            {
                return new List<Forecast>();
            }
        }
    }
}