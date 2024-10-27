using DogHouseService.Application.DTOs;
using MediatR;

namespace DogHouseService.Application.Queries
{
    public class GetDogsQuery : IRequest<IEnumerable<DogDto>>
    {
        public string SortAttribute { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
