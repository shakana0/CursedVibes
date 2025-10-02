using CursedVibes.Domain.ValueObjects;

namespace CursedVibes.Application.Characters.Dtos
{
    public class SceneDto
    {
        public required string SceneId { get; set; }
        public required string SceneType { get; set; }
        public required string Chapter { get; set; }
        public required string Background { get; set; }
        public List<string> Narration { get; } = new List<string>();
        public List<SceneChoices> Choices { get; } = new List<SceneChoices>();
        public List<string> Effects { get; } = new List<string>();
    }
}
