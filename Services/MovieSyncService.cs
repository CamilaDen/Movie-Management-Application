using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class MovieSyncService : IMovieSyncService
    {
        private readonly ISwapApiService _swapApiService;
        private readonly IMovieRepository _movieRepository;

        public MovieSyncService(ISwapApiService swapApiService,IMovieRepository movieRepository)
        {
            _swapApiService = swapApiService;
            _movieRepository = movieRepository;
        }

        public async Task<int> SyncMoviesAsync()
        {
            var swMovies = await _swapApiService.GetFilmsAsync();
            int count = 0;

            foreach (var movie in swMovies)
            {             
                var existingMovie = await _movieRepository.GetByExternalIdAsync(movie.ExternalId ?? 0);

                if (existingMovie == null)
                {                   
                    await _movieRepository.CreateAsync(movie);
                }
                else
                {
                    await _movieRepository.UpdateAsync(movie);
                }
                count++;
            }

            return count;
        }
    }
}
