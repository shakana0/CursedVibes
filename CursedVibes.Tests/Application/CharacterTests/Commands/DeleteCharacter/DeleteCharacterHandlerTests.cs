
using CursedVibes.Application.Characters.Commands.DeleteCharacter;
using CursedVibes.Application.Exceptions;
using CursedVibes.Domain.Interfaces;
using CursedVibes.Tests.Mocks;
using Moq;

namespace CursedVibes.Tests.Application.CharacterTests.Commands.DeleteCharacter
{
    public class DeleteCharacterHandlerTests
    {
        private readonly ICharacterRepository _characterRepository;

        public DeleteCharacterHandlerTests()
        {
            _characterRepository = new FakeCharacterRepository();
        }

        [Fact]
        public async Task Handle_GivenValidDeleteCharacterCommand_DeletesCharacter()
        {
            var character = TestDataGenerator.CreateCharacter(id: 3);
            await _characterRepository.CreateAsync(character, CancellationToken.None);

            var command = new DeleteCharacterCommand(character.Id);

            var handler = new DeleteCharacterHandler(_characterRepository);
            var result = await handler.Handle(command, CancellationToken.None);
            var deletedCharacter = await _characterRepository.GetByIdAsync(command.Id, CancellationToken.None);

            // Assert that character was deleted from repository
            Assert.Null(deletedCharacter);
            Assert.Equal(MediatR.Unit.Value, result);
        }

        [Fact]
        public async Task Handle_GivenNonExistentCharacterId_ThrowsNotFoundException()
        {
            var command = new DeleteCharacterCommand(55);

            var handler = new DeleteCharacterHandler(_characterRepository);

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            Assert.Equal($"Character with id {command.Id} not found", exception.Message);
        }

        [Fact]
        public async Task Handle_GivenValidDeleteCharacterCommand_CallsRepositoryMethodAndReturnsExpectedValue()
        {
            var command = new DeleteCharacterCommand(5);

            var repositoryMock = new Mock<ICharacterRepository>();
            repositoryMock
                 .Setup(r => r.DeleteAsync(command.Id, It.IsAny<CancellationToken>()))
                 .Returns(Task.FromResult(true));

            var handler = new DeleteCharacterHandler(repositoryMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            repositoryMock.Verify(r => r.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(MediatR.Unit.Value, result);
        }
    }
}
