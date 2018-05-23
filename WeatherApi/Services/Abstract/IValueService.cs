using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public interface IValueService
    {
        void AddValue(string value);

        ICollection<string> GetValues();

        Task<WeatherLocation> GetDescription(string zip);
    }
}