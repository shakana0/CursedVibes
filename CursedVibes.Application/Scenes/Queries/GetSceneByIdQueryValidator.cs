using FluentValidation;

namespace CursedVibes.Application.Scenes.Queries
{
    public class GetSceneByIdQueryValidator: AbstractValidator<GetSceneByIdQuery>
    {
        public GetSceneByIdQueryValidator() 
        {
            RuleFor(x => x.SceneId)
            .NotEmpty().WithMessage("SceneId is required.")
            .MinimumLength(3).WithMessage("SceneId must be at least 3 characters.")
            .MaximumLength(100).WithMessage("SceneId must be less than 100 characters.")
            .Matches("^[a-zA-Z0-9_-]+$").WithMessage("SceneId contains invalid characters.");
        }
    }
}
