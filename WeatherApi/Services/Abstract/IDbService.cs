using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public interface IDbService
    {
        Task Initialize();

        WeatherLocation ZipSearch(string zip);

        void AddLocation(WeatherLocation weather);

        Task ReplaceWeatherDocument(string zip, WeatherLocation locationUpdate);
    }
}