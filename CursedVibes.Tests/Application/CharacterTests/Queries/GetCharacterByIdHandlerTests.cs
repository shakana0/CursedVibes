using AutoMapper;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Application.Characters.Queries.GetCharacter;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;
using CursedVibes.Tests.Mocks;
using CursedVibes.Tests.TestUtils;
using Moq;


namespace CursedVibes.Tests.Application.CharacterTests.Queries;

public class GetCharacterByIdHandlerTests
{
    private readonly ICharacterRepository _characterRepository;

    public GetCharacterByIdHandlerTests()
    {
        _characterRepository = new FakeCharacterRepository();
    }

    [Fact]
    public async Task Handle_WhenCharacterExists_ReturnsCorrectDto()
    {
        //Arrange
        var newCharacter = TestDataGenerator.CreateCharacter(id: 4, name: "test 4");

        await _characterRepository.CreateAsync(newCharacter, CancellationToken.None);
        var query = new GetCharacterByIdQuery(newCharacter.Id);

        var mapperMock = new Mock<IMapper>();
        mapperMock
            .Setup(m => m.Map<CharacterDto>(It.IsAny<Character>()))
            .Returns((Character c) => TestDataGenerator.CreateCharacterDto(c));


        var handler = new GetCharacterByIdHandler(_characterRepository, mapperMock.Object);
        //Act
        var result = await handler.Handle(query, CancellationToken.None);
        //Assert
        Assert.NotNull(result);
        result.ShouldBeEqualTo(newCharacter);
    }
}
