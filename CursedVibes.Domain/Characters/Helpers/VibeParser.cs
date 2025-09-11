using static CursedVibes.Domain.Enums.VibeTypeFilter;

namespace CursedVibes.Domain.Characters.Helpers
{
    public static class VibeParser
    {
        public static List<VibeTypeEnum> Parse(string? vibeString)
        {
            if (string.IsNullOrWhiteSpace(vibeString))
                return new List<VibeTypeEnum>();

            var result = new List<VibeTypeEnum>();

            foreach (var raw in vibeString.Split(','))
            {
                var trimmed = raw.Trim();
                if (string.IsNullOrWhiteSpace(trimmed)) continue;

                if (Enum.TryParse<VibeTypeEnum>(trimmed, true, out var parsed))
                {
                    result.Add(parsed);
                }
            }

            return result.Distinct().ToList();
        }
        public static string NormalizeVibeTypeString(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            var parsed = Parse(input); // List<VibeTypeEnum>

            var normalized = parsed
                .Select(v => v.ToString().Substring(0, 1).ToUpper() + v.ToString().Substring(1).ToLower())
                .ToList();

            return string.Join(", ", normalized);
        }
    }
}