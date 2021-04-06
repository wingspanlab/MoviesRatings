using Microsoft.Extensions.Logging;
using MoviesRatingsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoviesRatingsApp.Common
{
    public interface ISWAPIService
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<Movie> GetMovie(int Id);
        Task<People> GetPeople(string url);
        Task<List<People>> GetPeopleList(IEnumerable<string> urls);
        Task<Planet> GetPlanet(string url);
        Task<List<Planet>> GetPlanetList(IEnumerable<string> urls);
        Task<Starship> GetStarship(string url);
        Task<List<Starship>> GetStarshipList(IEnumerable<string> urls);
        Task<Vehicle> GetVehicle(string url);
        Task<List<Vehicle>> GetVehicleList(IEnumerable<string> urls);
        Task<Species> GetSpecies(string url);
        Task<List<Species>> GetSpeciesList(IEnumerable<string> urls);
    }
    public class SWAPIService : ISWAPIService
    {
        private readonly HttpClient _client;
        private readonly ILogger<SWAPIService> _logger;

        public SWAPIService(HttpClient client, ILogger<SWAPIService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            SWAPITypeList<Movie> result;
            var hc = new HttpCaller<SWAPITypeList<Movie>>(_client);
            try
            {
                result = await hc.CallGetAsync("https://swapi.dev/api/films/");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Swapi Service issue");
                throw;
            }


            return result.results;
        }

        public async Task<Movie> GetMovie(int Id)
        {
            Movie result;
            var hc = new HttpCaller<Movie>(_client);
            try
            {
                result = await hc.CallGetAsync(string.Format("https://swapi.dev/api/films/{0}/", Id.ToString()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Swapi Service issue");
                throw;
            }

            return result;
        }

        public async Task<People> GetPeople(string url)
        {
            var hc = new HttpCaller<People>(_client);

            var result = await hc.CallGetAsync(url);

            return result;
        }

        public async Task<List<People>> GetPeopleList(IEnumerable<string> urls)
        {
            var entityList = new List<People>();
            foreach (string url in urls)
            {
                People p = await this.GetPeople(url);
                entityList.Add(p);
            }

            return entityList;
        }

        public async Task<Planet> GetPlanet(string url)
        {
            var hc = new HttpCaller<Planet>(_client);

            var result = await hc.CallGetAsync(url);

            return result;
        }

        public async Task<List<Planet>> GetPlanetList(IEnumerable<string> urls)
        {
            var entityList = new List<Planet>();
            foreach (string url in urls)
            {
                Planet p = await this.GetPlanet(url);
                entityList.Add(p);
            }

            return entityList;
        }

        public async Task<Starship> GetStarship(string url)
        {
            var hc = new HttpCaller<Starship>(_client);

            var result = await hc.CallGetAsync(url);

            return result;
        }

        public async Task<List<Starship>> GetStarshipList(IEnumerable<string> urls)
        {
            var entityList = new List<Starship>();
            foreach (string url in urls)
            {
                Starship p = await this.GetStarship(url);
                entityList.Add(p);
            }

            return entityList;
        }

        public async Task<Vehicle> GetVehicle(string url)
        {
            var hc = new HttpCaller<Vehicle>(_client);

            var result = await hc.CallGetAsync(url);

            return result;
        }

        public async Task<List<Vehicle>> GetVehicleList(IEnumerable<string> urls)
        {
            var entityList = new List<Vehicle>();
            foreach (string url in urls)
            {
                Vehicle p = await this.GetVehicle(url);
                entityList.Add(p);
            }

            return entityList;
        }

        public async Task<Species> GetSpecies(string url)
        {
            var hc = new HttpCaller<Species>(_client);

            var result = await hc.CallGetAsync(url);

            return result;
        }

        public async Task<List<Species>> GetSpeciesList(IEnumerable<string> urls)
        {
            var entityList = new List<Species>();
            foreach (string url in urls)
            {
                Species p = await this.GetSpecies(url);
                entityList.Add(p);
            }

            return entityList;
        }



    }
}
