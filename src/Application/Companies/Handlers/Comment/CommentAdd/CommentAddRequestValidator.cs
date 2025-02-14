using CompanyRatingApi.Shared.Context;
using CompanyRatingApi.Shared.Validators;
using FluentValidation;

namespace CompanyRatingApi.Application.Companies.Handlers.Comment.CommentAdd;

public class CommentAddRequestValidator : AbstractValidator<CommentAddRequest>
{
    public CommentAddRequestValidator(
        CurrentUserExistsValidator currentUserExistsValidator,
        CurrentUser currentUser
    )
    {
        RuleFor(_ => currentUser).SetValidator(currentUserExistsValidator).DependentRules(() =>
        {
            RuleFor(x => x.Content).NotEmpty().MaximumLength(5000);
        });
    }
}