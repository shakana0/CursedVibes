
using AutoMapper;
using CursedVibes.Application.Characters.Commands.UpdateCharacter;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Application.Exceptions;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;
using CursedVibes.Tests.Mocks;
using Moq;

namespace CursedVibes.Tests.Application.CharacterTests.Commands.UpdateCharacte
{
    public class UpdateCharacterHandlerTests
    {
        private readonly ICharacterRepository _characterRepository;

        public UpdateCharacterHandlerTests()
        {
            _characterRepository = new FakeCharacterRepository();
        }


        [Fact]
        public async Task Handle_GivenValidUpdateCharacterCommand_UpdatesCharacterAndReturnsUpdatedDto()
        {
            var newCharacter = TestDataGenerator.CreateCharacter(id: 3, name: "Test 3");
            await _characterRepository.CreateAsync(newCharacter, CancellationToken.None);
            var command = TestDataGenerator.UpdateCharacterCommand(id: 3, name: "Test updated");
            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(m => m.Map<CharacterDto>(It.IsAny<Character>()))
                .Returns((Character c) => TestDataGenerator.CreateCharacterDto(c));

            var handler = new UpdateCharacterHandler(_characterRepository, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            var updatedCharacter = await _characterRepository.GetByIdAsync(command.Id, CancellationToken.None);
            Assert.NotNull(result);
            Assert.NotNull(updatedCharacter);
            Assert.Equal("Test updated", updatedCharacter.Name);
            Assert.Equal("Test updated", result.Name);
        }

        [Fact]
        public async Task Handle_GivenNonExistentCharacterId_ThrowsNotFoundException()
        {
            var command = TestDataGenerator.UpdateCharacterCommand(id: 11);
            var handler = new UpdateCharacterHandler(_characterRepository, new Mock<IMapper>().Object);

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal($"Character with id {command.Id} not found", exception.Message);
        }

        [Fact]
        public async Task Handle_GivenValidUpdateCharacterCommand_CallsRepositoryMethodsAndReturnsUpdatedDto()
        {
            var character = TestDataGenerator.CreateCharacter(id: 5, name: "Test 5");
            await _characterRepository.CreateAsync(character, CancellationToken.None);

            var command = TestDataGenerator.UpdateCharacterCommand(id: 5, name: "Test Updated");

            var mapperMock = new Mock<IMapper>();
            mapperMock
            .Setup(m => m.Map<CharacterDto>(It.IsAny<Character>()))
            .Returns((Character c) => TestDataGenerator.CreateCharacterDto(c));

            var repositoryMock = new Mock<ICharacterRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(character);

            repositoryMock
               .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
               .Returns(Task.CompletedTask);

            var handler = new UpdateCharacterHandler(repositoryMock.Object, mapperMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);
            var updatedCharacter = await _characterRepository.GetByIdAsync(command.Id, CancellationToken.None);


            repositoryMock.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(result);
            Assert.NotNull(updatedCharacter);

            Assert.Equal(command.Name, updatedCharacter.Name);

        }
    }
}
