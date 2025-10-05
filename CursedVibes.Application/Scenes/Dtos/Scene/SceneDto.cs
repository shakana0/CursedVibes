namespace CursedVibes.Application.Scenes.Dtos.Scene
{
    public class SceneDto
    {
        public required string SceneId { get; set; }
        public required string SceneType { get; set; }
        public required string Chapter { get; set; }
        public SceneAuraDto SceneAura { get; set; } = new SceneAuraDto();
        public List<string> Narration { get; } = new List<string>();
        public List<SceneChoicesDto> Choices { get; } = new List<SceneChoicesDto>();
        public List<string> Effects { get; } = new List<string>();
    }
}
