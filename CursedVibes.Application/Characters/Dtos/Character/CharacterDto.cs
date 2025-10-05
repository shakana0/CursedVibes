namespace CursedVibes.Application.Characters.Dtos.Character
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CurseLevel { get; set; }
        public required string VibeType { get; set; }
        public required string BackStory { get; set; }
        // Stats directly in DTO
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Luck { get; set; }
    }
}
