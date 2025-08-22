namespace CursedVibes.Domain.ValueObjects
{
    public class CharacterStats
    {
        public int Strength { get; }
        public int Agility { get; }
        public int Intelligence { get; }
        public int Luck { get; }

        public CharacterStats() { }

        public CharacterStats(int strength, int agility, int intelligence, int luck)
        {
            if (strength < 0 || agility < 0 || intelligence < 0 || luck < 0)
                throw new ArgumentException("Stats cannot be negative.");

            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            Luck = luck;
        }

        // Returns a new version with all stats increased
        public CharacterStats IncreaseAll(int amount)
        {
            return new CharacterStats(
                Strength + amount,
                Agility + amount,
                Intelligence + amount,
                Luck + amount
            );
        }

        // Compare two Stats object
        public bool IsStrongerThan(CharacterStats other)
        {
            return TotalPower() > other.TotalPower();
        }

        // Sums up all Stats
        public int TotalPower()
        {
            return Strength + Agility + Intelligence + Luck;
        }

        // Is Stats balanced?
        public bool IsBalanced()
        {
            int[] values = { Strength, Agility, Intelligence, Luck };
            int max = values.Max();
            int min = values.Min();
            return (max - min) <= 2; // Example rule: max difference 2 = balanced
        }

        // Value equality
        public override bool Equals(object obj)
        {
            if (obj is not CharacterStats other) return false;
            return Strength == other.Strength &&
                   Agility == other.Agility &&
                   Intelligence == other.Intelligence &&
                   Luck == other.Luck;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Strength, Agility, Intelligence, Luck);
        }
    }
}
