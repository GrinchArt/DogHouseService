using DogHouseService.Application.DTOs;
using MediatR;

namespace DogHouseService.Application.Commands
{
    public record CreateDogCommand(string Name, string Color, int TailLength, int Weight) : IRequest<DogDto>;

}
