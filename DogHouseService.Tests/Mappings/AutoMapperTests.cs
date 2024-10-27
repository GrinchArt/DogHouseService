using AutoMapper;
using DogHouseService.Application.Commands;
using DogHouseService.Application.Mappings;
using DogHouseService.Domain.Entities;

namespace DogHouseService.Tests.Mappings
{
    public class AutoMapperTests
    {
        private readonly IMapper _mapper;

        public AutoMapperTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DogMappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_CreateDogCommand_To_Dog()
        {
            // Arrange
            var command = new CreateDogCommand("Buddy", "Brown", 20, 30);

            // Act
            var dog = _mapper.Map<Dog>(command);

            // Assert
            Assert.Equal(command.Name, dog.Name);
            Assert.Equal(command.Color, dog.Color);
            Assert.Equal(command.TailLength, dog.TailLength);
            Assert.Equal(command.Weight, dog.Weight);
        }
    }
}
