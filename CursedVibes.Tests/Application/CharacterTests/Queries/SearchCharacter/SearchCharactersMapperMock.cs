using AutoMapper;
using CursedVibes.Application.Characters.Dtos.Character;
using CursedVibes.Application.Characters.Queries.SearchCharacters;
using CursedVibes.Domain.Characters.Filters;
using CursedVibes.Domain.Characters.Helpers;
using CursedVibes.Domain.Entities;
using CursedVibes.Tests.Mocks;
using Moq;

namespace CursedVibes.Tests.Application.CharacterTests.Queries.SearchCharacter
{
    public static class SearchCharactersMapperMock
    {
        public static Mock<IMapper> Create()
        {
            var mock = new Mock<IMapper>();
            mock
            .Setup(m => m.Map<CharacterSearchFilter>(It.IsAny<SearchCharactersQuery>()))
            .Returns((SearchCharactersQuery q) => new CharacterSearchFilter()
            {
                Name = q.Name,
                VibeType = VibeParser.Parse(q.VibeType),
                Stat = q.Stat,
                MinStatValue = q.MinStatValue,
                Page = q.Page,
                PageSize = q.PageSize

            });

            mock
                .Setup(m => m.Map<List<CharacterDto>>(It.IsAny<IEnumerable<Character>>()))
                .Returns((IEnumerable<Character> entities) => entities.Select(TestDataGenerator.CreateCharacterDto).ToList());

            return mock;
        }
    }
}