using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Weather.Domain.Entities;

namespace Weather.Domain.Services.YrNo
{
    /// <summary>
    /// Class for communicating with yr.no web service
    /// </summary>
    public class YrNoWebService : IYrNoWebService
    {
        /// <summary>
        /// Search for forecasts matching place region and place name
        /// </summary>
        /// <param name="region">Region of the place</param>
        /// <param name="name">Name of the place</param>
        /// <returns>A Place object</returns>
        public Place GetPlaceForecast(string region, string name)
        {
            try
            {
                string xmlRaw = String.Empty;
                region = System.Uri.EscapeDataString(region);
                name = System.Uri.EscapeDataString(name);
                string url = String.Format("http://www.yr.no/sted/Sverige/{0}/{1}/forecast.xml", region, name);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    xmlRaw = reader.ReadToEnd();
                }
                return CreateForecastList(XDocument.Parse(xmlRaw), region);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Private helper method for extracting properties from XML response
        /// </summary>
        /// <param name="xml">Xml document to extract properties from</param>
        /// <param name="region">Region of the place</param>
        /// <returns>A Place object</returns>
        private Place CreateForecastList(XDocument xml, string region)
        {
            return new Place()
            {
                Name = xml.Element("weatherdata").Element("location").Element("name").Value,
                Region = region,
                Country = xml.Element("weatherdata").Element("location").Element("country").Value,
                Latitude = Convert.ToDouble(xml.Element("weatherdata").Element("location").Element("location").Attribute("latitude").Value, new CultureInfo("en-US")),
                Longitude = Convert.ToDouble(xml.Element("weatherdata").Element("location").Element("location").Attribute("longitude").Value, new CultureInfo("en-US")),
                NextUpdate = DateTime.Parse(xml.Element("weatherdata").Element("meta").Element("nextupdate").Value),
                Forecasts = (from forecast in xml.Descendants("time")
                             select new Forecast
                             {
                                 Period = Convert.ToInt32(forecast.Attribute("period").Value),
                                 StartTime = DateTime.Parse(forecast.Attribute("from").Value),
                                 EndTime = DateTime.Parse(forecast.Attribute("to").Value),
                                 SymbolNbr = Convert.ToByte(forecast.Element("symbol").Attribute("number").Value),
                                 SymbolTxt = forecast.Element("symbol").Attribute("name").Value,
                                 Temperatur = Convert.ToDouble(forecast.Element("temperature").Attribute("value").Value, new CultureInfo("en-US"))
                             }).ToList()
            };
        }
    }
}
