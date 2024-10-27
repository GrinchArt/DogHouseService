using AutoMapper;
using DogHouseService.Application.DTOs;
using DogHouseService.Domain.Entities;
using DogHouseService.Domain.Interfaces;
using MediatR;

namespace DogHouseService.Application.Commands
{
    public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, DogDto>
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;

        public CreateDogCommandHandler(IDogRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
        }

        public async Task<DogDto> Handle(CreateDogCommand request, CancellationToken cancellationToken)
        {
            // Check the dog with such name
            if (await _dogRepository.ExistsByNameAsync(request.Name))
            {
                throw new ArgumentException("A dog with the same name already exists.");
            }

            var dog = _mapper.Map<Dog>(request);
            await _dogRepository.AddAsync(dog);

            return _mapper.Map<DogDto>(dog);
        }
    }
}
