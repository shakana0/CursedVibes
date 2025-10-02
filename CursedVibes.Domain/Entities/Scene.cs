using CursedVibes.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace CursedVibes.Domain.Entities
{
    public class Scene
    {
        public string SceneId { get; } = string.Empty;
        public string SceneType { get; } = string.Empty;
        public string Chapter { get; } = string.Empty;
        public string Background { get; } = string.Empty;
        public List<string> Narration { get; } = new List<string>();
        public List<SceneChoices> Choices { get; } = new List<SceneChoices>();
        public List<string> Effects { get; } = new List<string>();

        [JsonConstructor]
        public Scene(string sceneId, string sceneType, string chapter, string background, List<string> narration, List<SceneChoices> choices, List<string> effects)
        {
            SceneId = sceneId;
            SceneType = sceneType;
            Chapter = chapter;
            Background = background;
            Narration = narration;
            Choices = choices;
            Effects = effects;
        }
    }
}
