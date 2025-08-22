using AutoMapper;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Domain.Entities;

namespace CursedVibes.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, CharacterDto>()
                .ForMember(dest => dest.Strength, opt => opt.MapFrom(src => src.Stats.Strength))
                .ForMember(dest => dest.Agility, opt => opt.MapFrom(src => src.Stats.Agility))
                .ForMember(dest => dest.Intelligence, opt => opt.MapFrom(src => src.Stats.Intelligence))
                .ForMember(dest => dest.Luck, opt => opt.MapFrom(src => src.Stats.Luck));
        }
    }
}
