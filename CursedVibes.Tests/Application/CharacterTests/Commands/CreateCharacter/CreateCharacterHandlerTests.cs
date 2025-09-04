
using AutoMapper;
using CursedVibes.Application.Characters.Commands.CreateCharacter;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;
using CursedVibes.Domain.ValueObjects;
using CursedVibes.Tests.Mocks;
using Moq;

namespace CursedVibes.Tests.Application.CharacterTests.Commands.CreateCharacter
{
    public class CreateCharacterHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCreateCharacterCommand_CallsRepositoryMethodsWithExpectedInput()
        {
            var command = TestDataGenerator.CreateCharacterCommand();

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(m => m.Map<Character>(It.IsAny<CreateCharacterCommand>()))
                .Returns((CreateCharacterCommand c) => TestDataGenerator.CreateCharacter(
                    name: c.Name,
                    curseLevel: c.CurseLevel,
                    vibeType: c.VibeType,
                    backStory: c.BackStory,
                    stats: new CharacterStats(c.Strength, c.Agility, c.Intelligence, c.Luck)
                ));

            var repositoryMock = new Mock<ICharacterRepository>();
            repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<Character>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateCharacterHandler(repositoryMock.Object, mapperMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(MediatR.Unit.Value, result);
            repositoryMock.Verify(r => r.CreateAsync(It.Is<Character>(c =>
               c.Name == command.Name &&
               c.CurseLevel == command.CurseLevel &&
               c.VibeType == command.VibeType &&
               c.BackStory == command.BackStory &&
               c.Stats.Strength == command.Strength &&
               c.Stats.Agility == command.Agility &&
               c.Stats.Intelligence == command.Intelligence &&
               c.Stats.Luck == command.Luck
           ), It.IsAny<CancellationToken>()), Times.Once);
            repositoryMock.VerifyNoOtherCalls();
        }

    }
}
