using CursedVibes.Domain.Characters.Helpers;
using FluentValidation;

namespace CursedVibes.Application.Characters.Queries.SearchCharacters
{
  public class SearchCharactersQueryValidator : AbstractValidator<SearchCharactersQuery>
  {
    public SearchCharactersQueryValidator()
    {
      RuleFor(x => x.Name)
        .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
        .When(x => !string.IsNullOrWhiteSpace(x.Name));

      RuleFor(x => x.VibeType)
          .Must(v =>
          {
            if (string.IsNullOrWhiteSpace(v)) return true;

            var parsed = VibeParser.Parse(v);
            var rawCount = v.Split(',').Select(x => x.Trim()).Count(x => !string.IsNullOrWhiteSpace(x));

            return parsed.Count == rawCount;
          })
          .WithMessage("VibeType must contain only valid values from the enum.");

      RuleFor(x => x.Stat)
        .NotNull()
        .When(x => x.MinStatValue.HasValue)
        .WithMessage("Stat must be specified when MinStatValue is set.");

      RuleFor(x => x.MinStatValue)
          .GreaterThanOrEqualTo(0)
          .When(x => x.MinStatValue.HasValue)
          .WithMessage("MinStatValue must be 0 or greater.");

      RuleFor(x => x.Page)
        .GreaterThan(0)
        .WithMessage("Page must be at least 1.");

      RuleFor(x => x.PageSize)
          .InclusiveBetween(1, 100)
          .WithMessage("PageSize must be between 1 and 100.");
    }
  }
}