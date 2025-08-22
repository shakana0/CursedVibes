using CursedVibes.Application.Characters.Dtos;
using MediatR;

namespace CursedVibes.Application.Characters.Queries.GetCharacter
{
    public class GetCharacterByIdQuery : IRequest<CharacterDto>
    {
        public int Id { get; set; }

        public GetCharacterByIdQuery(int id)
        {
            Id = id;
        }
    }
}
