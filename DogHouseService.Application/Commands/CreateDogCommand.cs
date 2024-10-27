using DogHouseService.Application.DTOs;
using MediatR;

namespace DogHouseService.Application.Commands
{
    public class CreateDogCommand : IRequest<DogDto>
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}
