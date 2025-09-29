using Models.Entities;

namespace Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> CreateAsync(Movie movie);
        Task<bool> UpdateAsync(Movie movie);
        Task<bool> DeleteAsync(int id);
        Task<Movie?> GetByIdAsync(int id);
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByExternalIdAsync(int externalId);
    }
}
