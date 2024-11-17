using AutoMapper;
using MovieCharacterAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MovieCharacterAPI.DTO.FranchiseDTOs;

public class FranchiseProfile : Profile
{
    public FranchiseProfile()
    {
        // Mapping between Franchise and ReadFranchiseDto
        CreateMap<Franchise, ReadFranchiseDto>();
        // Mapping between CreateFranchiseDto and Franchise
        CreateMap<CreateFranchiseDto, Franchise>();
        // Mapping between UpdateFranchiseDto and Franchise
        CreateMap<UpdateFranchiseDto, Franchise>();
    }
}