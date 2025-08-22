using AutoMapper;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Application.Exceptions;
using CursedVibes.Domain.Interfaces;

using MediatR;

namespace CursedVibes.Application.Characters.Queries.GetCharacter
{
    public class GetCharacterByIdHandler : IRequestHandler<GetCharacterByIdQuery, CharacterDto>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        public GetCharacterByIdHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        public async Task<CharacterDto> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
        {
            var character = await _characterRepository.GetByIdAsync(request.Id, cancellationToken);
            if (character == null)
            {
                throw new NotFoundException("Character", $"{request.Id}");
            }
            return _mapper.Map<CharacterDto>(character);
        }
    }
}
