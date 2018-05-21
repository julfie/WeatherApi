using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public class DbService : IDbService
    {
        private const string EndpointUri = "https://weather-api.documents.azure.com:443/";
        private const string PrimaryKey = "XJGxOf4ImFeOA63zxN609aqZhGfVOquxiCsqPr8y1KHTuVPClUmf7Pg0lE1O0yI0GE55eJAZh4HtIqEDWaelHg==";
        private static readonly string DatabaseId = "WeatherInfo";
        private static readonly string CollectionId = "Locations";
        private DocumentClient client;

        public async Task Initialize()
        {
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseId });
            await this.client.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(DatabaseId),
                    new DocumentCollection { Id = CollectionId },
                    new RequestOptions { OfferThroughput = 400 }
                );

            // test by adding fake information and printing it
            WeatherLocation newLocation = new WeatherLocation
            {
                Id = "0",
                Zip = "0",
                Name = "Not Redmond",
                Description = "a refreshing void",
                Temp = "over 9000",
                Humidity = "100",
                Date = DateTime.Today.Date
            };
            AddLocation(newLocation);
            ListWeatherLocations();
        }

        // add a new location to the db
        public async void AddLocation(WeatherLocation newWeatherLocation)
        {
            await CreateWeatherDocumentIfNotExists(newWeatherLocation);
        }

        private async Task CreateWeatherDocumentIfNotExists(WeatherLocation weather)
        {
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, weather.Id));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), weather);
                }
                else
                {
                    throw;
                }
            }
        }

        // search based on zip code
        public WeatherLocation ZipSearch(string zip)
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<WeatherLocation> WeatherQuery = this.client.CreateDocumentQuery<WeatherLocation>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                $"SELECT * FROM WeatherInfo WHERE WeatherInfo.Zip = '{zip}'",
                queryOptions);

            // HACK:: I assume there is only one returned. This assumption should not be made
            foreach (WeatherLocation location in WeatherQuery)
            {
                //if (location.Date == DateTime.Today.Date)
                //{
                    return location;
                //}
                //else
                //{
                //    return null;
                //}
            }
            return null;
        }

        public async Task ReplaceWeatherDocument(string zip, WeatherLocation locationUpdate)
        {
            await this.client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, zip), locationUpdate);
            Console.WriteLine("Replaced Family {0}", zip);
        }

        private void ListWeatherLocations()
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<WeatherLocation> WeatherQuery = this.client.CreateDocumentQuery<WeatherLocation>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                "SELECT * FROM WeatherInfo",
                queryOptions);

            Console.WriteLine("Running direct SQL query...");
            // printing all the locations currently saved
            foreach (WeatherLocation loc in WeatherQuery)
            {
                Console.WriteLine("\tRead {0}", loc.Name);
            }
        }
    }
}