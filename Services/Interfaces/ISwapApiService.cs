using Models.Entities;

namespace Services.Interfaces
{
    public interface ISwapApiService
    {
        Task<List<Movie>> GetFilmsAsync();
        Task<Movie> GetMovieAsync(int id);
    }
}
