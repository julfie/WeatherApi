using Microsoft.Azure.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public interface IDbService
    {
        Task Initialize();

        WeatherLocation ZipSearch(string zip);

        void AddLocation(WeatherLocation weather);

    }
}
