namespace CursedVibes.Domain.ValueObjects
{
    public class PlayerNarrativeState
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        // Stats
        public int TrustX { get; private set; } = 0;
        public int TrustY { get; private set; } = 0;
        public string EmotionalState { get; private set; } = "neutral";
        public int CurseLevel { get; private set; } = 0;
        public int Health { get; private set; } = 100;
        public string? Status { get; private set; }
        public string? ChatTone { get; private set; }
        public int DesireLevel { get; private set; } = 0;
        public int Corruption { get; private set; } = 0;


        // Progression
        public string LastScene { get; private set; } = "intro";
        public string LastChoice { get; private set; } = "";

        // Public methods to update stats
        public void UpdateTrust(string character, int delta)
        {
            if (character == "X") TrustX += delta;
            else if (character == "Y") TrustY += delta;
        }

        public void UpdateEmotionalState(string newState)
        {
            EmotionalState = newState;
        }

        public void UpdateCurseLevel(int delta)
        {
            CurseLevel += delta;
        }

        public void UpdateHealth(int delta)
        {
            Health = Math.Clamp(Health + delta, 0, 100);
        }

        public void RecordChoice(string choiceId, string sceneId)
        {
            LastChoice = choiceId;
            LastScene = sceneId;
        }

    }
}
