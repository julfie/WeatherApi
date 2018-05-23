using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WeatherApi.Services
{
    public class List
    {
        public IEnumerable<WeatherDescription> Weather { get; set; }
        public Main Main { get; set; }

        [JsonProperty(PropertyName = "dt_txt")]
        public string Date { get; set; }
    }
}