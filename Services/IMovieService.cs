using MovieCharacterAPI.Models;

namespace MovieCharacterAPI.Services
{
    public interface IMovieService
    {
        // Retrieve all movies
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        // Retrieve a movie by its ID
        Task<Movie> GetMovieByIdAsync(int id);
        // Create a new movie
        Task<Movie> CreateMovieAsync(Movie movie);
        // Update an existing movie
        Task<Movie> UpdateMovieAsync(Movie movie);
        // Delete a movie by its ID
        Task<bool> DeleteMovieAsync(int id);
    }
}
