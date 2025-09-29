using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace MovieManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private ISwapApiService _swapApiService;

        public MoviesController(ISwapApiService swapApiService)
        {
            _swapApiService = swapApiService;
        }

        /// <summary>
        /// Gets all movies
        /// </summary>
        /// <returns>List of all movies</returns>
        /// <response code="200">Returns the list of movies</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMovies()
        {
            var response = _swapApiService.GetFilmsAsync().Result;
            return Ok(response);
        }

        /// <summary>
        /// Gets a specific movie by id
        /// </summary>
        /// <param name="id">The movie id</param>
        /// <returns>Movie details</returns>
        /// <response code="200">Returns the movie details</response>
        /// <response code="404">If the movie was not found</response>
        /// <response code="403">If the user is not authorized</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Regular")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetMovie(int id)
        {
            var response = _swapApiService.GetMovieAsync(id).Result;
            return Ok(response);
        }

        /// <summary>
        /// Creates a new movie
        /// </summary>
        /// <returns>The created movie</returns>
        /// <response code="201">Returns the newly created movie</response>
        /// <response code="400">If the movie data is invalid</response>
        /// <response code="403">If the user is not authorized</response>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateMovie()
        {
            return CreatedAtAction(nameof(GetMovie), new { id = 1 }, null);
        }

        /// <summary>
        /// Updates an existing movie
        /// </summary>
        /// <param name="id">The movie id to update</param>
        /// <returns>No content</returns>
        /// <response code="204">If the movie was successfully updated</response>
        /// <response code="404">If the movie was not found</response>
        /// <response code="400">If the movie data is invalid</response>
        /// <response code="403">If the user is not authorized</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateMovie(int id)
        {
            return NoContent();
        }
    
        /// <summary>
        /// Deletes a specific movie
        /// </summary>
        /// <param name="id">The movie id to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the movie was successfully deleted</response>
        /// <response code="404">If the movie was not found</response>
        /// <response code="403">If the user is not authorized</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            return NoContent();
        }

        /// <summary>
        /// Synchronizes the movie list with Star Wars API
        /// </summary>
        /// <returns>Result of the synchronization</returns>
        /// <response code="200">If the synchronization was successful</response>
        /// <response code="403">If the user is not authorized</response>
        /// <response code="500">If there was an error during synchronization</response>
        [HttpPost("sync-star-wars")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SyncStarWarsMovies()
        {
            return Ok();
        }
    }
}
