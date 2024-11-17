using MovieCharacterAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using MovieCharacterAPI.Data;

namespace MovieCharacterAPI.Services
{
    

    public class MovieService : IMovieService
    {
        private readonly MovieCharacterDbContext _context;

        public MovieService(MovieCharacterDbContext context)
        {
            _context = context;
        }

        // Retrieve all movies from the database
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.Include(m => m.Characters).Include(m => m.Franchise).ToListAsync();
        }

        // Retrieve a specific movie by its ID
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _context.Movies.Include(m => m.Characters).Include(m => m.Franchise).FirstOrDefaultAsync(m => m.Id == id);
        }

        // Create a new movie
        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        // Update an existing movie
        public async Task<Movie> UpdateMovieAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return null;
                }
                throw;
            }
            return movie;
        }

        // Delete a movie by its ID
        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return false;
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if a movie exists by its ID
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
