using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApi.Models;
using WeatherApi.Services;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class WeatherController : Controller
    {
        private readonly IValueService valueService;

        public WeatherController(IValueService valueService)
        {
            this.valueService = valueService;
        }

        // GET api/weather
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return valueService.GetValues();
        }

        // GET api/weather/zip/55555
        [HttpGet("zip/{zip}")]
        public async Task<WeatherLocation> GetDescription(string zip)
        {
            WeatherLocation weather = await valueService.GetDescription(zip);
            return weather;
        }

        // POST api/weather
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/weather/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Data data)
        {
            this.valueService.AddValue(data.Value);
        }

        // DELETE api/weather/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Data
    {
        public string Value { get; set; }
    }
}