using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApi.Models;
using WeatherApi.Services;

namespace WeatherApi.Utilities
{
    public class WeatherHelper
    {
        public static async Task<WeatherLocation> GetWeatherDescription(string zip)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync($"http://api.openweathermap.org/data/2.5/weather?zip={zip}&appid=23abca4662290892ff42dd59246c00e1").Result;
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<OpenWeatherResponse>(json);
                var description = string.Join(",", weatherData.Weather.Select(x => x.Description));

                WeatherLocation weather = new WeatherLocation
                {
                    Id = zip,
                    Zip = zip,
                    Name = weatherData.Name,
                    Description = description,
                    Temp = weatherData.Main.Temp,
                    Humidity = weatherData.Main.Humidity,
                    Date = DateTime.Today.Date
                };

                return weather;
            }
        }
    }
}