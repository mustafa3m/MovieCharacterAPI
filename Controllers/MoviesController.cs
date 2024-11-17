using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MovieCharacterAPI.Services;
using MovieCharacterAPI.Models;
using MovieCharacterAPI.DTO.MovieDTOs;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IMapper _mapper;

    public MoviesController(IMovieService movieService, IMapper mapper)
    {
        _movieService = movieService;
        _mapper = mapper;
    }

    
    /// <summary>
    /// Gets all movies.
    /// </summary>
    /// <returns>A list of movies.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadMovieDto>>> GetMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();
        return Ok(_mapper.Map<IEnumerable<ReadMovieDto>>(movies));
    }

   
    /// <summary>
    /// Gets a specific movie by ID.
    /// </summary>
    /// <param name="id">The ID of the movie.</param>
    /// <returns>The requested movie.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ReadMovieDto>> GetMovie(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<ReadMovieDto>(movie));
    }

   
    /// <summary>
    /// Creates a new movie.
    /// </summary>
    /// <param name="createMovieDto">The movie to create.</param>
    /// <returns>The created movie.</returns>
    [HttpPost]
    public async Task<ActionResult<ReadMovieDto>> CreateMovie(CreateMovieDto createMovieDto)
    {
        var movie = await _movieService.CreateMovieAsync(_mapper.Map<Movie>(createMovieDto));
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, _mapper.Map<ReadMovieDto>(movie));
    }

    
    /// <summary>
    /// Updates an existing movie.
    /// </summary>
    /// <param name="id">The ID of the movie to update.</param>
    /// <param name="updateMovieDto">The updated movie information.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, UpdateMovieDto updateMovieDto)
    {
        if (id != updateMovieDto.Id)
        {
            return BadRequest();
        }

        var movie = await _movieService.UpdateMovieAsync(_mapper.Map<Movie>(updateMovieDto));
        if (movie == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    
    /// <summary>
    /// Deletes a movie.
    /// </summary>
    /// <param name="id">The ID of the movie to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var result = await _movieService.DeleteMovieAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
