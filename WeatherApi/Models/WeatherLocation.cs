using Newtonsoft.Json;
using System;

namespace WeatherApi.Models
{
    public class WeatherLocation
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Zip")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Forecasts")]
        public Forecast[] Forecasts { get; set; }
    }
}