using CursedVibes.Application.Exceptions;
using CursedVibes.Domain.Interfaces;
using MediatR;

namespace CursedVibes.Application.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterHandler : IRequestHandler<DeleteCharacterCommand, Unit>
    {
        private readonly ICharacterRepository _characterRepository;

        public DeleteCharacterHandler(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<Unit> Handle(DeleteCharacterCommand command, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.DeleteAsync(command.Id, cancellationToken);
            if (!character)
            {
                throw new NotFoundException("Character", $"{command.Id}");
            }

            return Unit.Value;
        }
    }
}