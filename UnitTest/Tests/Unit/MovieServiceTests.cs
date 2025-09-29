using Models.Entities;
using Moq;
using Repository.Interfaces;
using Services;

namespace UnitTest.Tests.Unit
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly MovieService _service;

        public MovieServiceTests()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _service = new MovieService(_mockMovieRepository.Object);
        }

        [Fact]
        public async Task GetAllMoviesAsync_ReturnsAllMovies()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1" },
                new Movie { Id = 2, Title = "Movie 2" }
            };
            _mockMovieRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(movies);

            // Act
            var result = await _service.GetAllMoviesAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetMovieByIdAsync_ExistingId_ReturnsMovie()
        {
            // Arrange
            var movie = new Movie { Id = 1, Title = "Movie 1" };
            _mockMovieRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(movie);

            // Act
            var result = await _service.GetMovieByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetMovieByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            _mockMovieRepository.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Movie)null);

            // Act
            var result = await _service.GetMovieByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateMovieAsync_ValidMovie_ReturnsCreatedMovie()
        {
            // Arrange
            var movie = new Movie { Title = "New Movie" };
            var createdMovie = new Movie { Id = 1, Title = "New Movie" };
            _mockMovieRepository.Setup(r => r.CreateAsync(movie)).ReturnsAsync(createdMovie);

            // Act
            var result = await _service.CreateMovieAsync(movie);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task UpdateMovieAsync_ExistingMovie_ReturnsTrue()
        {
            // Arrange
            var movie = new Movie { Id = 1, Title = "Updated" };
            _mockMovieRepository.Setup(r => r.UpdateAsync(movie)).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateMovieAsync(movie);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateMovieAsync_NonExistingMovie_ReturnsFalse()
        {
            // Arrange
            var movie = new Movie { Id = 99, Title = "Not Found" };
            _mockMovieRepository.Setup(r => r.UpdateAsync(movie)).ReturnsAsync(false);

            // Act
            var result = await _service.UpdateMovieAsync(movie);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteMovieAsync_ExistingId_ReturnsTrue()
        {
            // Arrange
            _mockMovieRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteMovieAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteMovieAsync_NonExistingId_ReturnsFalse()
        {
            // Arrange
            _mockMovieRepository.Setup(r => r.DeleteAsync(99)).ReturnsAsync(false);

            // Act
            var result = await _service.DeleteMovieAsync(99);

            // Assert
            Assert.False(result);
        }
    }
}