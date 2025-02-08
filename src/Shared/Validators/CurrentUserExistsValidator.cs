using CompanyRatingApi.Shared.Context;
using CompanyRatingApi.Shared.Persistence;
using FluentValidation;

namespace CompanyRatingApi.Shared.Validators;

public class CurrentUserExistsValidator : AbstractValidator<CurrentUser>
{
    public CurrentUserExistsValidator(
        ApplicationDbContext dbContext
    )
    {
        RuleFor(user => user).NotNull().DependentRules(() => {
                RuleFor(user => user.Id).NotEmpty()
                    .DependentRules(() => {
                        RuleFor(user => user.Id).Must((id) => {
                                return dbContext.AppUsers.Any(x => x.Id == id);
                            }).WithMessage("User does not exist");
                    });
            });
    }
}