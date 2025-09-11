using static CursedVibes.Domain.Enums.StatTypeFilter;
using static CursedVibes.Domain.Enums.VibeTypeFilter;

namespace CursedVibes.Domain.Characters.Filters
{
    public class CharacterSearchFilter
    {
        public string? Name { get; set; }
        public List<VibeTypeEnum>? VibeType { get; set; }
        public StatTypeEnum? Stat { get; set; }
        public int? MinStatValue { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}