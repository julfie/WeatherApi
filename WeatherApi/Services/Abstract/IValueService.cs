using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApi.Services
{
    public interface IValueService
    {
        void AddValue(string value);

        ICollection<string> GetValues();

        Task<string> GetDescription(string zip);
    }
}