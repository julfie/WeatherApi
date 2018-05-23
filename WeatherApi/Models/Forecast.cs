using Newtonsoft.Json;
using System;

namespace WeatherApi.Models
{
    public class Forecast
    {
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Temp")]
        public string Temp { get; set; }

        [JsonProperty(PropertyName = "Humidity")]
        public string Humidity { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "Icon")]
        public string Icon { get; set; }
    }
}