
using FluentValidation;

namespace CursedVibes.Application.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");

            RuleFor(x => x.VibeType)
                .NotEmpty().WithMessage("VibeType is required.");

            RuleFor(x => x.BackStory)
                .NotEmpty().WithMessage("BackStory is required.")
                .MinimumLength(10).WithMessage("BackStory must be at least 10 characters long.");

            RuleFor(x => x.CurseLevel)
                .InclusiveBetween(0, 100).WithMessage("CurseLevel must be between 0 and 100.");

            RuleFor(x => x.Strength)
                .InclusiveBetween(1, 100).WithMessage("Strength must be between 1 and 100.");

            RuleFor(x => x.Agility)
                .InclusiveBetween(1, 100).WithMessage("Agility must be between 1 and 100.");

            RuleFor(x => x.Intelligence)
                .InclusiveBetween(1, 100).WithMessage("Intelligence must be between 1 and 100.");

            RuleFor(x => x.Luck)
                .InclusiveBetween(1, 100).WithMessage("Luck must be between 1 and 100.");
        }
    }
}
