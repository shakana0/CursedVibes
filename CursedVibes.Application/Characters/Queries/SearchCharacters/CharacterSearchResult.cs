using CursedVibes.Application.Characters.Dtos.Character;

namespace CursedVibes.Application.Characters.Queries.SearchCharacters
{
    public class CharacterSearchResult
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => CurrentPage * PageSize < TotalCount;
        public bool HasPreviousPage => CurrentPage > 1;
        public int PageSize { get; set; }
        public List<CharacterDto> Characters { get; set; } = new();
    }
}
