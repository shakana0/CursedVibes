namespace CursedVibes.Application.Scenes.Dtos.Scene
{
    public class SceneChoicesDto
    {
        public string Text { get; set; } = string.Empty;
        public string Tone { get; set; } = string.Empty;
        public string Outcome { get; set; } = string.Empty;
        public string NextScene { get; set; } = string.Empty;
        public StatDeltaDto StatImpact { get; set; } = new StatDeltaDto();
    }
}
