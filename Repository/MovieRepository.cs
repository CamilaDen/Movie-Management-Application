using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enums;
using Repository.Interfaces;

namespace Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> UpdateAsync(Movie movie)
        {
            var existingMovie = await _context.Movies.FindAsync(movie.Id);
            if (existingMovie == null) return false;

            _context.Entry(existingMovie).CurrentValues.SetValues(movie);
            var updatedRows = await _context.SaveChangesAsync();
            return updatedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            movie.Status = MovieStatus.Inactive;
            _context.Movies.Update(movie);
            var updatedRows = await _context.SaveChangesAsync();
            return updatedRows > 0;
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Where(m => m.Id == id && m.Status == MovieStatus.Active)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Where(m => m.Status == MovieStatus.Active)
                .ToListAsync();
        }

        public Task<Movie?> GetByExternalIdAsync(int externalId)
        {
            return _context.Movies.FirstOrDefaultAsync(m => m.ExternalId == externalId);
        }
    }
}
