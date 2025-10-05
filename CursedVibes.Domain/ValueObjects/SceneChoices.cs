namespace CursedVibes.Domain.ValueObjects
{
    public record SceneChoices(
           string Text,
           string Tone,
           string Outcome,
           string NextScene,
           StatDelta StatImpact
           );
}
