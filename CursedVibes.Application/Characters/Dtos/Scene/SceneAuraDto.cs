

namespace CursedVibes.Application.Characters.Dtos.Scene
{
    public class SceneAuraDto
    {
        public string Background { get; set; } = string.Empty;
        public string? AmbientAudio { get; set; }
        public string? Music { get; set; }
        public List<string>? Sfx { get; set; }
    }

}
