using CursedVibes.Domain.Entities;
using CursedVibes.Application.Characters.Dtos;

namespace CursedVibes.Tests.TestUtils
{
    public static class AssertExtensions
    {
        public static void ShouldBeEqualTo(this CharacterDto actual, Character expected)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.CurseLevel, actual.CurseLevel);
            Assert.Equal(expected.VibeType, actual.VibeType);
            Assert.Equal(expected.BackStory, actual.BackStory);

            Assert.Equal(expected.Stats.Strength, actual.Strength);
            Assert.Equal(expected.Stats.Intelligence, actual.Intelligence);
            Assert.Equal(expected.Stats.Agility, actual.Agility);
            Assert.Equal(expected.Stats.Luck, actual.Luck);
        }
    }
}
