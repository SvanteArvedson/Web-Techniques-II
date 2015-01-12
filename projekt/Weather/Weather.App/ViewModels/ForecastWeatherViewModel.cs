using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Weather.Domain;
using Weather.Domain.Entities;

namespace Weather.App.ViewModels
{
    /// <summary>
    /// Containing data about forecast to show on the web page
    /// Containing logic to transform Weather.Domain.Forecast objects to WeatherForecastViewData objects
    /// </summary>
    public class ForecastWeatherViewModel
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Forecast> ForecastList { get; set; }
        public List<WeatherForecastViewData> Forecasts { get; set; }

        public ForecastWeatherViewModel(Place place)
        {
            Name = place.Name;
            Region = place.Region;
            Latitude = place.Latitude;
            Longitude = place.Longitude;
            ForecastList = place.Forecasts.ToList();
            Forecasts = new List<WeatherForecastViewData>(5);

            for (int i = 0; i <= 4; i += 1)
            {
                Forecast forecast = getForecast(i);

                string day = forecast.StartTime.ToString("dddd", new CultureInfo("sv-SE"));
                string imageUrl = String.Format("//api.yr.no/weatherapi/weathericon/1.1/?symbol={0};content_type=image/png", forecast.SymbolNbr);
                string imageAlt = forecast.SymbolTxt;
                double temperature = forecast.Temperatur;

                Forecasts.Add(new WeatherForecastViewData() { 
                    Day = day,
                    ImageUrl = imageUrl,
                    ImageAlt = imageAlt,
                    Temperature = temperature
                });
            }
        }

        /// <summary>
        /// Private helper method to extract properties from Weather.Domain.Forecast object
        /// </summary>
        /// <param name="daysInFuture">0-4 days in future</param>
        /// <returns>A Forecast object</returns>
        private Forecast getForecast(int daysInFuture)
        {
            DateTime targetDate = DateTime.Now.AddDays(daysInFuture);

            Forecast forecast = (ForecastList.FirstOrDefault(x => x.StartTime.Date == targetDate.Date && x.Period == 2));
            if (forecast == null)
            {
                forecast = ForecastList.FirstOrDefault(x => x.StartTime.Date == targetDate.Date && x.Period == 3);
            }
            if (forecast == null)
            {
                forecast = ForecastList.FirstOrDefault(x => x.StartTime.Date == targetDate.Date.AddDays(1).Date && x.Period == 2);
            }

            return forecast;
        }
    }
}