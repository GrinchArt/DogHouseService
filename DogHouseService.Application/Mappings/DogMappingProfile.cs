using AutoMapper;
using DogHouseService.Application.Commands;
using DogHouseService.Application.DTOs;
using DogHouseService.Domain.Entities;

namespace DogHouseService.Application.Mappings
{
    public class DogMappingProfile : Profile
    {
        public DogMappingProfile()
        {
            CreateMap<Dog, DogDto>().ReverseMap();
            CreateMap<CreateDogCommand, Dog>();
        }
    }
}
