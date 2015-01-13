using System.Collections.Generic;
using Weather.Domain.Entities;

namespace Weather.App.ViewModels
{
    /// <summary>
    /// Containing data about a search to show on the web page
    /// </summary>
    public class ForecastSearchViewModel
    {
        /// <summary>
        /// Places matching the search word
        /// </summary>
        public List<Place> Places { get; set; }
    }
}