using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
                string url = $"http://api.openweathermap.org/data/2.5/forecast?zip={zip}&appid=23abca4662290892ff42dd59246c00e1";

                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<OpenWeatherResponse>(json);

                // temp list of 5 day/ 3 hour forecasts
                var weatherForecasts = new List<Forecast>();

                // add all forecasts to the temp list as theyre created
                foreach (List timeWeather in weatherData.List)
                {
                    Main main = timeWeather.Main;
                    IEnumerable<WeatherDescription> weatherDesc = timeWeather.Weather;
                    string description = string.Join(",", weatherDesc.Select(x => x.Description));
                    string icon = string.Join(",", weatherDesc.Select(x => x.Icon));
                    string date = DateTime.Parse(timeWeather.Date).ToString("MM/dd h:mm tt");
                    Forecast forecast = new Forecast
                    {
                        Description = description,
                        Temp = main.Temp,
                        Humidity = main.Humidity,
                        Date = date,
                        Icon = icon
                    };
                    weatherForecasts.Add(forecast);
                }

                WeatherLocation weather = new WeatherLocation
                {
                    Id = zip,
                    Zip = zip,
                    Name = weatherData.City.Name,
                    Forecasts = weatherForecasts.ToArray()
                };

                return weather;
            }
        }
    }
}