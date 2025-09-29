using Models.Entities;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            return await _movieRepository.CreateAsync(movie);
        }

        public async Task<bool> UpdateMovieAsync(Movie movie)
        {
            return await _movieRepository.UpdateAsync(movie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            return await _movieRepository.DeleteAsync(id);
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _movieRepository.GetByIdAsync(id);
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllAsync();
        }
    }
}
