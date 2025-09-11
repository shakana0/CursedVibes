using AutoMapper;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;
using MediatR;

namespace CursedVibes.Application.Characters.Commands.CreateCharacter
{
    public class CreateCharacterHandler : IRequestHandler<CreateCharacterCommand, Unit>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public CreateCharacterHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = _mapper.Map<Character>(request);
            character.UpdateVibeType(request.VibeType);

            await _characterRepository.CreateAsync(character, cancellationToken);

            return Unit.Value;
        }
    }
}