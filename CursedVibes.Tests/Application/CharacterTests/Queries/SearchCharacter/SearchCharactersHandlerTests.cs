using AutoMapper;
using CursedVibes.Application.Characters.Dtos.Character;
using CursedVibes.Application.Characters.Queries.SearchCharacters;
using CursedVibes.Domain.Characters.Filters;
using CursedVibes.Domain.Characters.Helpers;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;
using CursedVibes.Tests.Mocks;
using CursedVibes.Tests.TestUtils;
using Moq;
using static CursedVibes.Domain.Enums.StatTypeFilter;
using static CursedVibes.Domain.Enums.VibeTypeFilter;


namespace CursedVibes.Tests.Application.CharacterTests.Queries.SearchCharacter
{
    public class SearchCharactersHandlerTests
    {
        private readonly ICharacterRepository _characterRepository;

        public SearchCharactersHandlerTests()
        {
            _characterRepository = new FakeCharacterRepository();
        }

        public async Task<List<Character>> AddCharacters()
        {
            var characters = TestDataGenerator.CreateCharacterList();
            if (characters.Count > 0)
            {
                foreach (var character in characters)
                {
                    await _characterRepository.CreateAsync(character, CancellationToken.None);
                }
            }
            return characters;
        }

        bool VibeTypesMatch(List<VibeTypeEnum>? a, List<VibeTypeEnum>? b)
        {
            return (a ?? Enumerable.Empty<VibeTypeEnum>())
                .SequenceEqual(b ?? Enumerable.Empty<VibeTypeEnum>());
        }

        bool FilterMatches(CharacterSearchFilter actual, CharacterSearchFilter expected)
        {
            return actual.Name == expected.Name &&
                   actual.Stat == expected.Stat &&
                   actual.MinStatValue == expected.MinStatValue &&
                   actual.Page == expected.Page &&
                   actual.PageSize == expected.PageSize &&
                   VibeTypesMatch(actual.VibeType, expected.VibeType);
        }


        [Fact]
        public async Task Handle_GivenValidSearchCharactersQuery_ReturnsCharacterSearchResult()
        {
            var characters = await AddCharacters();

            var query = new SearchCharactersQuery();

            var mapperMock = SearchCharactersMapperMock.Create();

            var handler = new SearchCharactersHandler(_characterRepository, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsType<CharacterSearchResult>(result);
            Assert.NotNull(result.Characters);
            Assert.NotEmpty(result.Characters);
            Assert.Equal(characters.Count, result.Characters.Count);
            Assert.Equal(characters.Count, result.TotalCount);
            Assert.All(result.Characters, dto =>
            Assert.Contains(dto.Id, characters.Select(c => c.Id)));
        }

        [Fact]
        public async Task Handle_GivenQueryWithName_ReturnsFilteredCharacterSearchResult()
        {
            var characters = await AddCharacters();

            var query = new SearchCharactersQuery()
            {
                Name = "1"
            };

            var mapperMock = SearchCharactersMapperMock.Create();

            var handler = new SearchCharactersHandler(_characterRepository, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            var expected = characters.Where(c => c.Name.Contains(query.Name)).ToList();

            Assert.IsType<CharacterSearchResult>(result);
            Assert.NotNull(result.Characters);
            Assert.NotEmpty(result.Characters);
            Assert.Equal(expected.Count, result.Characters.Count);
            Assert.All(result.Characters, dto =>
                Assert.Contains(query.Name, dto.Name));
        }

        [Fact]
        public async Task Handle_GivenQueryWithVibeType_ReturnsFilteredCharacterSearchResult()
        {
            var characters = await AddCharacters();

            var query = new SearchCharactersQuery()
            {
                VibeType = "Chaotic"
            };

            var mapperMock = SearchCharactersMapperMock.Create();

            var handler = new SearchCharactersHandler(_characterRepository, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            var expected = characters.Where(c => c.VibeType.Contains(query.VibeType)).ToList();

            Assert.IsType<CharacterSearchResult>(result);
            Assert.NotNull(result.Characters);
            Assert.NotEmpty(result.Characters);
            Assert.Equal(expected.Count, result.Characters.Count);

            for (int i = 0; i < result.Characters.Count; i++)
            {
                Assert.Equal(query.VibeType, result.Characters[i].VibeType);
            }
        }

        [Fact]
        public async Task Handle_GivenQueryWithStatAndMinStatValue_ReturnsFilteredCharacterSearchResult()
        {
            var characters = await AddCharacters();

            var query = new SearchCharactersQuery()
            {
                Stat = StatTypeEnum.Strength,
                MinStatValue = 10
            };

            var mapperMock = SearchCharactersMapperMock.Create();

            var handler = new SearchCharactersHandler(_characterRepository, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            var expected = characters.Where(c => c.Stats.Strength >= query.MinStatValue).ToList();

            Assert.IsType<CharacterSearchResult>(result);
            Assert.NotNull(result.Characters);
            Assert.NotEmpty(result.Characters);
            Assert.Equal(expected.Count, result.Characters.Count);

            for (int i = 0; i < result.Characters.Count; i++)
            {
                Assert.True(result.Characters[i].Strength >= query.MinStatValue);
            }
        }

        [Fact]
        public async Task Handle_GivenQueryWithPageSize_ReturnsExpectedNumberOfCharacters()
        {
            var characters = await AddCharacters();

            var query = new SearchCharactersQuery()
            {
                Page = 1,
                PageSize = 2
            };

            var mapperMock = SearchCharactersMapperMock.Create();

            var handler = new SearchCharactersHandler(_characterRepository, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsType<CharacterSearchResult>(result);
            Assert.NotNull(result.Characters);
            Assert.NotEmpty(result.Characters);
            Assert.Equal(query.PageSize, result.Characters.Count);
            for (int i = 0; i < result.Characters.Count; i++)
            {
                result.Characters[i].ShouldBeEqualTo(characters[i]);
            }
        }

        [Fact]
        public async Task Handle_GivenValidSearchCharactersQuery_CallsRepositoryMethodsWithExpectedInput()
        {
            var characters = await AddCharacters();
            var query = new SearchCharactersQuery();

            var expectedFilter = new CharacterSearchFilter
            {
                Name = query.Name,
                VibeType = VibeParser.Parse(query.VibeType),
                Stat = query.Stat,
                MinStatValue = query.MinStatValue,
                Page = query.Page,
                PageSize = query.PageSize
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(m => m.Map<CharacterSearchFilter>(query))
                .Returns(expectedFilter);

            mapperMock
                .Setup(m => m.Map<List<CharacterDto>>(It.IsAny<IEnumerable<Character>>()))
                .Returns((IEnumerable<Character> c) =>
                {
                    return c.Select(TestDataGenerator.CreateCharacterDto).ToList();
                });


            var repositoryMock = new Mock<ICharacterRepository>();
            repositoryMock
            .Setup(r => r.SearchAsync(It.IsAny<CharacterSearchFilter>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(characters);

            repositoryMock
            .Setup(r => r.CountAsync(It.IsAny<CharacterSearchFilter>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(characters.Count);

            var handler = new SearchCharactersHandler(repositoryMock.Object, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.IsType<CharacterSearchResult>(result);
            Assert.NotNull(result.Characters);
            Assert.NotEmpty(result.Characters);

            repositoryMock.Verify(r => r.SearchAsync(It.Is<CharacterSearchFilter>(f =>
            f.Name == query.Name &&
            VibeTypesMatch(f.VibeType, VibeParser.Parse(query.VibeType)) &&
            f.Stat == query.Stat &&
            f.MinStatValue == query.MinStatValue &&
            f.Page == query.Page &&
            f.PageSize == query.PageSize
            ), It.IsAny<CancellationToken>()), Times.Once);

            repositoryMock.Verify(r => r.CountAsync(
                It.Is<CharacterSearchFilter>(f => FilterMatches(f, expectedFilter)),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
