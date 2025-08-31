
using CursedVibes.Application.Characters.Commands.CreateCharacter;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.ValueObjects;

namespace CursedVibes.Tests.Mocks
{
    public static class TestDataGenerator
    {
        public static Character CreateCharacter(
            int id = 1,
            string name = "Test",
            int curseLevel = 50,
            string vibeType = "Chill",
            string backStory = "Raised by code and caffeine.",
            CharacterStats? stats = null
            )
        {
            stats ??= new CharacterStats(10, 10, 10, 10);

            var character = new Character(id, name, curseLevel, vibeType, backStory, stats);

            return character;
        }

        public static CreateCharacterCommand CreateCharacterCommand(
            string? name = "Test",
            int curseLevel = 50,
            string? vibeType = "Chill",
            string? backStory = "Raised by code and caffeine.",
            int strength = 10,
            int agility = 10,
            int intelligence = 10,
            int luck = 10
            )
        {

            return new CreateCharacterCommand
            {
                Name = name ?? string.Empty,
                CurseLevel = curseLevel,
                VibeType = vibeType ?? string.Empty,
                BackStory = backStory ?? string.Empty,
                Strength = strength,
                Agility = agility,
                Intelligence = intelligence,
                Luck = luck
            };
        }

        public static List<Character> CreateCharacterList(int count = 3)
        {
            var list = new List<Character>();
            var random = new Random();
            int number = random.Next(10, 101);
            for (int i = 1; i <= count; i++)
            {
                list.Add(CreateCharacter(
                    id: i,
                    name: $"Character {i}",
                    curseLevel: number,
                    vibeType: "Chill",
                    backStory: $"Backstory for Character {i}",
                    stats: new CharacterStats(i * 5, i * 3, i * 4, i * 2)
                ));
            }
            return list;
        }

        public static CharacterDto CreateCharacterDto(Character c)
        {
            return new CharacterDto
            {
                Id = c.Id,
                Name = c.Name,
                CurseLevel = c.CurseLevel,
                VibeType = c.VibeType,
                BackStory = c.BackStory,
                Strength = c.Stats.Strength,
                Agility = c.Stats.Agility,
                Intelligence = c.Stats.Intelligence,
                Luck = c.Stats.Luck
            };
        }
    }

}
