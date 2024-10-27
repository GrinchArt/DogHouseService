using AutoMapper;
using DogHouseService.Application.DTOs;
using DogHouseService.Domain.Entities;

namespace DogHouseService.Application.Mappings
{
    public class DogMappingProfile : Profile
    {
        public DogMappingProfile()
        {
            CreateMap<Dog, DogDto>()
                .ForMember(dest => dest.TailLength, opt => opt.MapFrom(src => src.TailLength.Value));
            CreateMap<DogDto, Dog>()
                .ForMember(dest => dest.TailLength, opt => opt.MapFrom(src => new TailLength(src.TailLength)));
        }
    }
}
