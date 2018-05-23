namespace WeatherApi.Services
{
    // descriptions of weather in specified area
    public class WeatherDescription
    {
        public string Description { get; set; }
        public string Main { get; set; }
        public string Icon { get; set; }
    }
}