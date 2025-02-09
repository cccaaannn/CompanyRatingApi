using CompanyRatingApi.Shared.Context;
using CompanyRatingApi.Shared.Validators;
using FluentValidation;

namespace CompanyRatingApi.Application.Companies.Handlers.Rating.CompanyRating;

public class CompanyRatingRequestValidator : AbstractValidator<CompanyRatingRequest>
{
    public CompanyRatingRequestValidator(
        CurrentUserExistsValidator currentUserExistsValidator,
        CurrentUser currentUser
    )
    {
        RuleFor(_ => currentUser).SetValidator(currentUserExistsValidator).DependentRules(() =>
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5);
        });
    }
}