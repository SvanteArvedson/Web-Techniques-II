namespace Weather.App.ViewModels
{
    /// <summary>
    /// Containing data about forecast to show on the web page
    /// </summary>
    public class WeatherForecastViewData
    {
        /// <summary>
        /// Weekday name
        /// </summary>
        public string Day { get; set; }
        
        /// <summary>
        /// URL to the weather symbol
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Alt text for the weather symbol (for the moment in Norwegian)
        /// </summary>
        public string ImageAlt { get; set; }

        /// <summary>
        /// Temperature
        /// </summary>
        public double Temperature { get; set; }
    }
}