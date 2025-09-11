using CursedVibes.Application.Characters.Queries.SearchCharacters;
using FluentValidation.TestHelper;
using static CursedVibes.Domain.Enums.StatTypeFilter;

namespace CursedVibes.Tests.Application.CharacterTests.Queries.SearchCharacter
{
    public class SearchCharactersQueryValidatorTests
    {
        private readonly SearchCharactersQueryValidator _validator;

        public SearchCharactersQueryValidatorTests()
        {
            _validator = new SearchCharactersQueryValidator();
        }

        [Fact]
        public void Validate_GivenValidSearchCharactersQuery_ReturnsNoValidationError()
        {
            var query = new SearchCharactersQuery()
            {
                Name = "Name",
                VibeType = "Brutal",
                Stat = StatTypeEnum.Strength,
                MinStatValue = 10
            };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("Test", true)]
        [InlineData(null, true)]
        [InlineData("xy", false)]
        public void Validate_Name_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(string? name, bool expectedIsValid)
        {
            var query = new SearchCharactersQuery()
            {
                Name = name,
            };

            var result = _validator.TestValidate(query);
            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.Name);
            else
                result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Theory]
        [InlineData("Brutal", true)]
        [InlineData(null, true)]
        [InlineData("Brutal,,", true)]
        [InlineData("Mystic,Stoic,Divine", true)]
        [InlineData("  CHILL  ,  Mystic  ", true)]
        [InlineData("   ", true)]
        [InlineData("Mystic,,Stoic", true)]
        [InlineData("Brutal, Test", false)]
        [InlineData("Test", false)]
        [InlineData("Mystic,Test,Unknown", false)]
        [InlineData("Mysticc", false)]
        public void Validate_VibeType_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(string? vibeType, bool expectedIsValid)
        {
            var query = new SearchCharactersQuery()
            {
                VibeType = vibeType,
            };

            var result = _validator.TestValidate(query);
            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.VibeType);
            else
                result.ShouldHaveValidationErrorFor(c => c.VibeType);
        }

        [Theory]
        [InlineData(null, null, true)]
        [InlineData(null, 5, false)]
        [InlineData(StatTypeEnum.Strength, null, true)]
        [InlineData(StatTypeEnum.Luck, 5, true)]
        public void Validate_Stat_WhenMinStatValueIsSet_ReturnsExpectedResult(StatTypeEnum? stat, int? minStatValue, bool expectedIsValid)
        {
            var query = new SearchCharactersQuery
            {
                Stat = stat,
                MinStatValue = minStatValue,
            };

            var result = _validator.TestValidate(query);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(x => x.Stat);
            else
                result.ShouldHaveValidationErrorFor(x => x.Stat);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, true)]
        [InlineData(-1, false)]
        public void Validate_MinStatValue_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(int minStatValue, bool expectedIsValid)
        {
            var query = new SearchCharactersQuery
            {
                Stat = StatTypeEnum.Agility,
                MinStatValue = minStatValue,
            };

            var result = _validator.TestValidate(query);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.MinStatValue);
            else
                result.ShouldHaveValidationErrorFor(c => c.MinStatValue);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void Validate_Page_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(int page, bool expectedIsValid)
        {
            var query = new SearchCharactersQuery()
            {
                Page = page,
            };

            var result = _validator.TestValidate(query);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.Page);
            else
                result.ShouldHaveValidationErrorFor(c => c.Page);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(100, true)]
        [InlineData(0, false)]
        [InlineData(101, false)]
        [InlineData(-1, false)]
        public void Validate_PageSize_WhenValueIsValidOrInvalid_ReturnsCorrectValidationError(int pageSize, bool expectedIsValid)
        {
            var query = new SearchCharactersQuery()
            {
                PageSize = pageSize
            };

            var result = _validator.TestValidate(query);

            if (expectedIsValid)
                result.ShouldNotHaveValidationErrorFor(c => c.PageSize);
            else
                result.ShouldHaveValidationErrorFor(c => c.PageSize);
        }
    }
}