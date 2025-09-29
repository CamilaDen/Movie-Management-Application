using Models.Entities;

namespace Services.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> CreateMovieAsync(Movie movie);
        Task<bool> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(int id);
    }
}
