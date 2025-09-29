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
            var response = list.Result.Select(MapFilmToMovie).ToList();
            return response;
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<SwapiSingleResponse<Film>>($"films/{id}");
            if (response.Result == null)
                return null;
            var movie = MapFilmToMovie(response.Result);
            return movie;
        }

        private Movie MapFilmToMovie(Film film)
        {
            return new Movie
            {
                ExternalId = int.Parse(film.Uid),
                Title = film.Properties.Title,
                EpisodeId = film.Properties.Episode_Id,
                Director = film.Properties.Director,
                Producer = film.Properties.Producer,
                ReleaseDate = DateTime.Parse(film.Properties.Release_Date),
                OpeningCrawl = film.Properties.Opening_Crawl,
                Description = film.Properties.Description
            };
        }
    }
}
