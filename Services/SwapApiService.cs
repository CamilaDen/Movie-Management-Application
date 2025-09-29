using Models.DTOs;
using Models.Entities;
using Services.Interfaces;
using System.Net.Http.Json;

namespace Services
{
    public class SwapApiService : ISwapApiService
    {
        private readonly HttpClient _httpClient;
        public SwapApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Movie>> GetFilmsAsync()
        {
            var list = await _httpClient.GetFromJsonAsync<SwapiResponse<Film>>("films");
            if (list?.Result == null)
                return new List<Movie>();
            var response = MapFilmsToMovies(list);
            return response;
        }

        private List<Movie> MapFilmsToMovies(SwapiResponse<Film> films)
        {
            return films.Result.Select(f => new Movie
            {
                Title = f.Properties.Title,
                EpisodeId = f.Properties.Episode_Id,
                Director = f.Properties.Director,
                Producer = f.Properties.Producer,
                ReleaseDate = DateTime.Parse(f.Properties.Release_Date),
                OpeningCrawl = f.Properties.Opening_Crawl,
                Description = f.Properties.Description
            }).ToList();
        }
    }
}
