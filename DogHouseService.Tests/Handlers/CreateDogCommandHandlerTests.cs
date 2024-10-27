using AutoMapper;
using DogHouseService.Application.Commands;
using DogHouseService.Application.Mappings;
using DogHouseService.Domain.Entities;
using DogHouseService.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Tests.Handlers
{
    public class CreateDogCommandHandlerTests
    {
        private readonly Mock<IDogRepository> _dogRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateDogCommandHandler _handler;

        public CreateDogCommandHandlerTests()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DogMappingProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new CreateDogCommandHandler(_dogRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldCreateDog_WhenNameIsUnique()
        {
            // Arrange
            var command = new CreateDogCommand
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 20,
                Weight = 30
            };

            _dogRepositoryMock.Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>())).ReturnsAsync(false);
            _dogRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Dog>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Name, result.Name);
            _dogRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Dog>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenNameAlreadyExists()
        {
            // Arrange
            var command = new CreateDogCommand
            {
                Name = "Buddy",
                Color = "Brown",
                TailLength = 20,
                Weight = 30
            };

            _dogRepositoryMock.Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>())).ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
