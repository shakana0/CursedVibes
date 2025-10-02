namespace CursedVibes.Domain.ValueObjects
{
    public class StatDelta
    {
        public int? Health { get; set; }
        public int? CurseLevel { get; set; }
        public string? Status { get; set; }
        public string? EmotionalState { get; set; }
        public int? DesireLevel { get; set; }
        public int? Corruption { get; set; }
        public string? ChatTone { get; set; }
        public Dictionary<string, int>? Trust { get; set; }

        public StatDelta() { }

        public StatDelta(int? health, int? curseLevel, string? status, string? emotionalState, int? desireLevel, int? corruption, string? chatTone, Dictionary<string, int>? trust)
        {
            Health = health;
            CurseLevel = curseLevel;
            Status = status;
            EmotionalState = emotionalState;
            DesireLevel = desireLevel;
            Corruption = corruption;
            ChatTone = chatTone;
            Trust = trust;
        }
    }

}
