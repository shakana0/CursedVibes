using AutoMapper;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Application.Exceptions;
using CursedVibes.Domain.Interfaces;
using MediatR;

namespace CursedVibes.Application.Characters.Commands.UpdateCharacter
{
    public class UpdateCharacterHandler : IRequestHandler<UpdateCharacterCommand, CharacterDto>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public UpdateCharacterHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        public async Task<CharacterDto> Handle(UpdateCharacterCommand command, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetByIdAsync(command.Id, cancellationToken);
            if (character == null)
                throw new NotFoundException("Character", $"{command.Id}");

            character.UpdateName(command.Name);
            character.UpdateCurseLevel(command.CurseLevel);
            character.UpdateVibeType(command.VibeType);
            character.UpdateBackStory(command.BackStory);
            character.UpdateStats(
                command.Strength,
                command.Agility,
                command.Intelligence,
                command.Luck); 

            await _characterRepository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CharacterDto>(character);
        }
    }
}
