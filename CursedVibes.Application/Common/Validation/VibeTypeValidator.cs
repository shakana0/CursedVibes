using CursedVibes.Domain.Characters.Helpers;

namespace CursedVibes.Application.Common.Validation
{
    public static class VibeTypeValidator
    {
        public static bool IsValidVibeType(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            var parsed = VibeParser.Parse(input);
            var rawCount = input.Split(',').Select(x => x.Trim()).Count(x => !string.IsNullOrWhiteSpace(x));

            return parsed.Count == rawCount && parsed.Count > 0;
        }
    }
}