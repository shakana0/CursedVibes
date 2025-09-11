using MediatR;
using static CursedVibes.Domain.Enums.StatTypeFilter;

namespace CursedVibes.Application.Characters.Queries.SearchCharacters
{
    public class SearchCharactersQuery : IRequest<CharacterSearchResult>
    {
        public string? Name { get; set; }
        public string? VibeType { get; set; }
        public StatTypeEnum? Stat { get; set; }
        public int? MinStatValue { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}