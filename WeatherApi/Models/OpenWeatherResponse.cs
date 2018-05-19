using System.Collections.Generic;

namespace WeatherApi.Services
{
    // C# object of weather information
    internal class OpenWeatherResponse
    {
        public string Name { get; set; }
        public IEnumerable<WeatherDescription> Weather { get; set; }
        public Main Main { get; set; }
    }
}