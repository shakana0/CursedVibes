
using CursedVibes.Application.Characters.Commands.UpdateCharacter;
using CursedVibes.Tests.Mocks;
using FluentValidation.TestHelper;

namespace CursedVibes.Tests.Application.CharacterTests.Commands.UpdateCharacte
{
    public class UpdateCharacterCommandValidatorTests
    {
        private readonly UpdateCharacterCommandValidator _validator;

        public UpdateCharacterCommandValidatorTests()
        {
            _validator = new UpdateCharacterCommandValidator();
        }

        [Fact]
        public void Validate_GivenValidUpdateCharacterCommand_ReturnsNoValidationErrors()
        {
            var command = TestDataGenerator.UpdateCharacterCommand();

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void Validate_Id_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(int id, bool expectedIsValid)
        {
            var command = TestDataGenerator.UpdateCharacterCommand(id: id);
            var result = _validator.TestValidate(command);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.Id);
            else
                result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Theory]
        [InlineData("Test", true)]
        [InlineData("", false)]
        [InlineData("     ", false)]
        [InlineData("xy", false)]
        [InlineData(null, false)]
        public void Validate_Name_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(string? name, bool expectedIsValid)
        {
            var command = TestDataGenerator.UpdateCharacterCommand(name: name);
            var result = _validator.TestValidate(command);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.Name);
            else
                result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Theory]
        [InlineData("Brutal", true)]
        [InlineData("Brutal,,", true)]
        [InlineData("Mystic,Stoic,Divine", true)]
        [InlineData("  Brutal  ,  MystIC  ", true)]
        [InlineData("Mystic,,Stoic", true)]
        [InlineData("Brutal, Test", false)]
        [InlineData("Test", false)]
        [InlineData("Mystic,Test,Unknown", false)]
        [InlineData("Mysticc", false)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData(null, false)]
        public void Validate_VibeType_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(string? vibeType, bool expectedIsValid)
        {
            var command = TestDataGenerator.UpdateCharacterCommand(vibeType: vibeType);
            var result = _validator.TestValidate(command);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.VibeType);
            else
                result.ShouldHaveValidationErrorFor(c => c.VibeType);
        }

        [Theory]
        [InlineData("Back story test", true)]
        [InlineData("", false)]
        [InlineData("   ", false)]
        [InlineData("xy", false)]
        [InlineData(null, false)]
        public void Validate_BackStory_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(string? backStory, bool expectedIsValid)
        {
            var command = TestDataGenerator.UpdateCharacterCommand(backStory: backStory);
            var result = _validator.TestValidate(command);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.BackStory);
            else
                result.ShouldHaveValidationErrorFor(c => c.BackStory);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(100, true)]
        [InlineData(101, false)]
        public void Validate_CurseLevel_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(int curseLevel, bool expectedIsValid)
        {
            var command = TestDataGenerator.UpdateCharacterCommand(curseLevel: curseLevel);
            var result = _validator.TestValidate(command);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.CurseLevel);
            else
                result.ShouldHaveValidationErrorFor(c => c.CurseLevel);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(100, true)]
        [InlineData(0, false)]
        [InlineData(101, false)]
        public void Validate_Stats_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(int value, bool expectedIsValid)
        {
            var command = TestDataGenerator.UpdateCharacterCommand(strength: value, agility: value, intelligence: value, luck: value);
            var result = _validator.TestValidate(command);

            if (expectedIsValid)
            {
                result.ShouldNotHaveValidationErrorFor(c => c.Strength);
                result.ShouldNotHaveValidationErrorFor(c => c.Agility);
                result.ShouldNotHaveValidationErrorFor(c => c.Intelligence);
                result.ShouldNotHaveValidationErrorFor(c => c.Luck);
            }

            else
            {
                result.ShouldHaveValidationErrorFor(c => c.Strength);
                result.ShouldHaveValidationErrorFor(c => c.Agility);
                result.ShouldHaveValidationErrorFor(c => c.Intelligence);
                result.ShouldHaveValidationErrorFor(c => c.Luck);
            }
        }
    }
}
