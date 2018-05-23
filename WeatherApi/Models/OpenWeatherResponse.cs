using System.Collections.Generic;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    // C# object of weather information
    internal class OpenWeatherResponse
    {
        public City City { get; set; }
        public IEnumerable<List> List { get; set; }
    }
}