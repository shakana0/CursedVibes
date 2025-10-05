namespace CursedVibes.Domain.ValueObjects
{
    public record StatDelta(
           int? Health,
           int? CurseLevel,
           string? Status,
           string? EmotionalState,
           int? DesireLevel,
           int? Corruption,
           string? ChatTone,
           Dictionary<string, int>? Trust
           );
}
