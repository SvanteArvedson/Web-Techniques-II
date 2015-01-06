using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Weather.Domain.Entities;

namespace Weather.Domain.Services.GeoNames
{
    /// <summary>
    /// Class for communicating with GeoNames web service
    /// </summary>
    public class GeoNamesWebService : IGeoNamesWebService
    {
        /// <summary>
        /// Search for places matching a search word
        /// </summary>
        /// <param name="search">The search word</param>
        /// <returns>A collection of Place objects</returns>
        public IEnumerable<Place> SerachPlace(string search)
        {
            try
            {
                string xmlRaw = String.Empty;
                search = System.Uri.EscapeDataString(search);
                string url = String.Format("http://api.geonames.org/search?username={0}&q={1}&country=SE&style=FULL&lang=en",
                    ConfigurationManager.AppSettings["GeoNamesUsername"], search);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                using (WebResponse response = request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    xmlRaw = reader.ReadToEnd();
                }
                return CreateSearchResult(XDocument.Parse(xmlRaw));
            }
            catch (Exception e)
            {
                return new List<Place>();
            }
        }

        /// <summary>
        /// Private helper method for extracting properties from XML response
        /// </summary>
        /// <param name="xml">Xml document to extract properties from</param>
        /// <returns>A collection of Place objects</returns>
        private IEnumerable<Place> CreateSearchResult(XDocument xml)
        {
            return (from geoname in xml.Descendants("geoname")
                    select new Place()
                    {
                        Country = "Sverige",
                        Region = geoname.Element("adminName1").Value,
                        Name = geoname.Element("toponymName").Value
                    }).ToList();
        }
    }
}