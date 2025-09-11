using CursedVibes.Domain.Characters.Filters;
using CursedVibes.Domain.Entities;
using static CursedVibes.Domain.Enums.StatTypeFilter;

namespace CursedVibes.Domain.Characters.Extensions
{
    public static class CharacterQueryExtensions
    {
        public static IQueryable<Character> ApplySearchFilter(this IQueryable<Character> query, CharacterSearchFilter filter)
        {
            if (filter == null) return query;

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(c => c.Name.Contains(filter.Name));

            if (filter.VibeType != null && filter.VibeType.Any())
            {
                var vibeStrings = filter.VibeType.Select(v => v.ToString());
                query = query.Where(c => vibeStrings.Any(v => c.VibeType.Contains(v)));
            }

            if (filter.Stat.HasValue && filter.MinStatValue.HasValue)
            {
                switch (filter.Stat.Value)
                {
                    case StatTypeEnum.Strength:
                        query = query.Where(c => c.Stats.Strength >= filter.MinStatValue.Value);
                        break;
                    case StatTypeEnum.Agility:
                        query = query.Where(c => c.Stats.Agility >= filter.MinStatValue.Value);
                        break;
                    case StatTypeEnum.Intelligence:
                        query = query.Where(c => c.Stats.Intelligence >= filter.MinStatValue.Value);
                        break;
                    case StatTypeEnum.Luck:
                        query = query.Where(c => c.Stats.Luck >= filter.MinStatValue.Value);
                        break;
                }
            }

            return query;
        }
    }
}