using AutoMapper;
using CursedVibes.Application.Characters.Commands.CreateCharacter;
using CursedVibes.Application.Characters.Dtos.Character;
using CursedVibes.Application.Characters.Queries.SearchCharacters;
using CursedVibes.Application.Scenes.Dtos.Scene;
using CursedVibes.Domain.Characters.Filters;
using CursedVibes.Domain.Characters.Helpers;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.ValueObjects;

namespace CursedVibes.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Character
            CreateMap<Character, CharacterDto>()
                .ForMember(dest => dest.Strength, opt => opt.MapFrom(src => src.Stats.Strength))
                .ForMember(dest => dest.Agility, opt => opt.MapFrom(src => src.Stats.Agility))
                .ForMember(dest => dest.Intelligence, opt => opt.MapFrom(src => src.Stats.Intelligence))
                .ForMember(dest => dest.Luck, opt => opt.MapFrom(src => src.Stats.Luck));

            CreateMap<CreateCharacterCommand, Character>()
                .ConstructUsing(src => new Character(
                    src.Name,
                    src.CurseLevel,
                    src.VibeType,
                    src.BackStory,
                    new CharacterStats(src.Strength, src.Agility, src.Intelligence, src.Luck)
                ));

            CreateMap<SearchCharactersQuery, CharacterSearchFilter>()
                    .ForMember(dest => dest.VibeType, opt => opt.MapFrom(src => VibeParser.Parse(src.VibeType)));
            
            //Scene
            CreateMap<Scene, SceneDto>();
            CreateMap<SceneAura, SceneAuraDto>();
            CreateMap<SceneChoices, SceneChoicesDto>();
            CreateMap<StatDelta, StatDeltaDto>();
        }
    }
}
