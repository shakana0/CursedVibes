using MediatR;

namespace CursedVibes.Application.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteCharacterCommand(int id)
        {
            Id = id;
        }
    }
}