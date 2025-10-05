namespace CursedVibes.Application.Scenes.Dtos.Scene
{
    public class StatDeltaDto
    {
        public int? Health { get; set; }
        public int? CurseLevel { get; set; }
        public string? Status { get; set; }
        public string? EmotionalState { get; set; }
        public int? DesireLevel { get; set; }
        public int? Corruption { get; set; }
        public string? ChatTone { get; set; }
        public Dictionary<string, int>? Trust { get; set; }
    }
}
