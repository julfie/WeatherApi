using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApi.Models;
using WeatherApi.Utilities;

namespace WeatherApi.Services
{
    public class ValueService : IValueService
    {
        // database context
        private readonly IDbService DbService;

        public ValueService(IDbService DbService)
        {
            this.DbService = DbService;
            this.DbService.Initialize();
        }

        private List<string> values = new List<string>() { "value1", "value2" };

        // add a value to list of values
        public void AddValue(string value)
        {
            values.Add(value);
        }

        // get all values
        public ICollection<string> GetValues()
        {
            return values;
        }

        // Getting the Name of the Zip code area
        public async Task<WeatherLocation> GetDescription(string zip)
        {
            try
            {
                WeatherLocation weather = this.DbService.ZipSearch(zip);
                Console.WriteLine(weather);
                if (weather == null)
                {
                    weather = await WeatherHelper.GetWeatherDescription(zip);
                    this.DbService.AddLocation(weather);
                }
                // TODO:: FIX THIS FOR REALS
                else if (weather.Forecasts[0].Date != DateTime.Today.ToString("MM/dd h:mm tt"))
                {
                    weather = await WeatherHelper.GetWeatherDescription(zip);
                    await this.DbService.ReplaceWeatherDocument(weather.Zip, weather);
                }
                return weather;
            }
            catch (HttpRequestException httpRequestException)
            {
                return null; // ($"Error getting weather from OpenWeather: {httpRequestException.Message}");
            }
        }
    }
}