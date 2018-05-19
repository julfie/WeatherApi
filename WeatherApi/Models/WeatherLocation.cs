using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "Temp")]
        public string Temp { get; set; }
        [JsonProperty(PropertyName = "Humidity")]
        public string Humidity { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
    }
}
