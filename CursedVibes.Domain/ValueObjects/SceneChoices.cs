using System.Text.Json.Serialization;

namespace CursedVibes.Domain.ValueObjects
{
    public class SceneChoices
    {
        public string Text { get; } = string.Empty;
        public string Outcome { get; } = string.Empty;
        public string NextScene { get; } = string.Empty;
        public StatDelta StatImpact { get; } = new StatDelta();

        [JsonConstructor]
        public SceneChoices(string text, string outcome, string nextScene, StatDelta statImpact)
        {
            Text = text;
            Outcome = outcome;
            NextScene = nextScene;
            StatImpact = statImpact;
        }
    }
}
