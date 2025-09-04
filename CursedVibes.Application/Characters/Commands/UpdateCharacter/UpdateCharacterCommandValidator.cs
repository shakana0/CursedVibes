
using FluentValidation;

namespace CursedVibes.Application.Characters.Commands.UpdateCharacter
{
    public class UpdateCharacterCommandValidator : AbstractValidator<UpdateCharacterCommand>
    {
        public UpdateCharacterCommandValidator()
        {
            RuleFor(c => c.Id)
            .GreaterThan(0)
            .WithMessage("Character ID must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name cannot be just whitespace.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");

            RuleFor(x => x.VibeType)
                .NotEmpty().WithMessage("VibeType is required.")
                .Must(vibeType => !string.IsNullOrWhiteSpace(vibeType)).WithMessage("VibeType cannot be just whitespace.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.");

            RuleFor(x => x.BackStory)
                .NotEmpty().WithMessage("BackStory is required.")
                .Must(backStory => !string.IsNullOrWhiteSpace(backStory)).WithMessage("BackStory cannot be just whitespace.")
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
