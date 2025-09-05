
using CursedVibes.Application.Characters.Commands.DeleteCharacter;
using FluentValidation.TestHelper;

namespace CursedVibes.Tests.Application.CharacterTests.Commands.DeleteCharacter
{
    public class DeleteCharacterCommandValidatorTests
    {
        private readonly DeleteCharacterCommandValidator _validator;

        public DeleteCharacterCommandValidatorTests()
        {
            _validator = new DeleteCharacterCommandValidator();
        }

        [Fact]
        public void Validate_GivenValidDeleteCharacterCommand_ReturnsNoValidationErrors()
        {
            var commnand = new DeleteCharacterCommand(5);
            var result = _validator.TestValidate(commnand);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void Validate_Id_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(int id, bool expectedIsValid)
        {
            var command = new DeleteCharacterCommand(id);
            var result = _validator.TestValidate(command);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.Id);
            else
                result.ShouldHaveValidationErrorFor(c => c.Id);
        }
    }
}
