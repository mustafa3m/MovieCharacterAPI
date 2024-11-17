using AutoMapper;
using MovieCharacterAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MovieCharacterAPI.DTO.CharacterDTOs;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        // Mapping between Character and ReadCharacterDto
        CreateMap<Character, ReadCharacterDto>();
        // Mapping between CreateCharacterDto and Character
        CreateMap<CreateCharacterDto, Character>();
        // Mapping between UpdateCharacterDto and Character
        CreateMap<UpdateCharacterDto, Character>();
    }
}
