using CursedVibes.Domain.ValueObjects;

namespace CursedVibes.Domain.Entities
{
    public class Character
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int CurseLevel { get; private set; }
        public string VibeType { get; private set; } = string.Empty;
        public string BackStory { get; private set; } = string.Empty;
        public CharacterStats Stats { get; private set; } = new CharacterStats();

        public Character() { }
        internal Character(int id, string name, int curseLevel, string vibeType, string backStory, CharacterStats stats)
            : this(name, curseLevel, vibeType, backStory, stats)
        {
            Id = id;
        }


        public Character(string name, int curseLevel, string vibeType, string backStory, CharacterStats stats)
        {
            Name = name;
            Stats = stats;
            CurseLevel = curseLevel;
            VibeType = vibeType;
            BackStory = backStory;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty.");

            Name = newName;
        }

        public void UpdateCurseLevel(int curseLevel)
        {
            if (curseLevel < 0)
                throw new ArgumentException("Curse level cannot be negative.");
            CurseLevel = curseLevel;
        }

        public void UpdateVibeType(string vibeType)
        {
            if (string.IsNullOrEmpty(vibeType))
                throw new ArgumentException("Vibe type cannot be empty.");
            VibeType = vibeType;
        }

        public void UpdateBackStory(string backStory)
        {
            if (string.IsNullOrEmpty(backStory))
                throw new ArgumentException("Back story cannot be empty.");
            BackStory = backStory;
        }
        public void UpdateStats(int strength, int agility, int intelligence, int luck)
        {

            Stats = new CharacterStats(strength, agility, intelligence, luck);
        }

        public void ApplyCurse(int amount = 1)
        {
            CurseLevel += amount;

            // Domain rule: CurseLevel must not exceed 100
            if (CurseLevel > 100)
            {
                CurseLevel = 100;
            }
        }

        public void LevelUp()
        {
            // Create a new version of stats with improved values
            Stats = Stats.IncreaseAll(1);
        }

        public bool IsCursed()
        {
            return CurseLevel > 50; // Domain rule: over 50 = "cursed"
        }
    }
}
