using AutoMapper;
using MovieCharacterAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MovieCharacterAPI.DTO.MovieDTOs;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        // Mapping between Movie and MovieDto
        CreateMap<Movie, ReadMovieDto>();
        // Mapping between CreateMovieDto and Movie
        CreateMap<CreateMovieDto, Movie>();
        // Mapping between UpdateMovieDto and Movie
        CreateMap<UpdateMovieDto, Movie>();
    }
}