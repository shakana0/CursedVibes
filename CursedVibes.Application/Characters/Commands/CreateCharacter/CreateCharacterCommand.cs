using MediatR;

namespace CursedVibes.Application.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommand : IRequest<Unit>
    {
        public required string Name { get; set; }
        public int CurseLevel { get; set; }
        public required string VibeType { get; set; }
        public required string BackStory { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Luck { get; set; }
    }
}

