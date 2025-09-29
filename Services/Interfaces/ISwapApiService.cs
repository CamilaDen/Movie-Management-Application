using Models.Entities;

namespace Services.Interfaces
{
    public interface ISwapApiService
    {
        Task<List<Movie>> GetFilmsAsync();
    }
}
