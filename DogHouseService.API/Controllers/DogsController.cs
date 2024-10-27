using DogHouseService.Application.Commands;
using DogHouseService.Application.DTOs;
using DogHouseService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DogHouseService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: /dogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DogDto>>> GetDogs(
            [FromQuery] string? attribute = null,
            [FromQuery] string order = "asc",
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new GetDogsQuery
            {
                SortAttribute = attribute,
                SortOrder = order,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST: /dogs
        [HttpPost]
        public async Task<ActionResult<DogDto>> CreateDog([FromBody] CreateDogCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDogs), new { name = result.Name }, result);
        }
    }
}
