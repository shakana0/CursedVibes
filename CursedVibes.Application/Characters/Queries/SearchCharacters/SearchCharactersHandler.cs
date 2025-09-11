using AutoMapper;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Domain.Characters.Filters;
using CursedVibes.Domain.Interfaces;
using MediatR;

namespace CursedVibes.Application.Characters.Queries.SearchCharacters
{
    public class SearchCharactersHandler : IRequestHandler<SearchCharactersQuery, CharacterSearchResult>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;

        public SearchCharactersHandler(ICharacterRepository characterRepository, IMapper mapper)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
        }

        public async Task<CharacterSearchResult> Handle(SearchCharactersQuery query, CancellationToken cancellationToken)
        {
            var searchParam = _mapper.Map<CharacterSearchFilter>(query);
            
            var result = await _characterRepository.SearchAsync(searchParam, cancellationToken);
            var totalCount = await _characterRepository.CountAsync(searchParam, cancellationToken);

            var characterDtos = _mapper.Map<List<CharacterDto>>(result);

            return new CharacterSearchResult
            {
                Characters = characterDtos,
                CurrentPage = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
