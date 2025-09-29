using Microsoft.EntityFrameworkCore;
using Models.Entities;
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

            _context.Movies.Remove(movie);
            var deletedRows = await _context.SaveChangesAsync();
            return deletedRows > 0;
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }
    }
}
