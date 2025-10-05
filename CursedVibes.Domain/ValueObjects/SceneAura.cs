namespace CursedVibes.Domain.ValueObjects
{
    public record SceneAura(
        string Background,
        string? AmbientAudio,
        string? Music,
        List<string>? Sfx
        );
}
