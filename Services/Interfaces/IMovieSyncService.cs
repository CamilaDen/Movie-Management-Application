namespace Services.Interfaces
{
    public interface IMovieSyncService
    {
        Task<int> SyncMoviesAsync();
    }
}
