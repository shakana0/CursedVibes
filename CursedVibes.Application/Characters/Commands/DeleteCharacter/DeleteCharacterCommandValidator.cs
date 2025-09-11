using FluentValidation;

namespace CursedVibes.Application.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterCommandValidator : AbstractValidator<DeleteCharacterCommand>
    {
        public DeleteCharacterCommandValidator()
        {
            RuleFor(c => c.Id)
            .GreaterThan(0)
            .WithMessage("Character ID must be greater than zero.");
        }
    }
}