using DogHouseService.Application.DTOs;
using MediatR;

namespace DogHouseService.Application.Queries
{
    public record GetDogsQuery(string? SortAttribute = null, string SortOrder = "asc", int PageNumber = 1, int PageSize = 10) : IRequest<IEnumerable<DogDto>>;

}
