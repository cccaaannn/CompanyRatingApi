using FluentValidation;

namespace CompanyRatingApi.Application.Companies.Handlers.CompanyUpdate;

public class CompanyUpdateRequestValidator : AbstractValidator<CompanyUpdateRequest>
{
    public CompanyUpdateRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(1000);

        RuleFor(x => x.Industry).IsInEnum();

        RuleFor(x => x.Description).MaximumLength(5000);

        RuleFor(x => x.Address).MaximumLength(1000);

        RuleFor(x => x.City).MaximumLength(500);

        RuleFor(x => x.Country).MaximumLength(500);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().MaximumLength(1000)
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Website)
            .NotEmpty()
            .MaximumLength(1000).Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .When(x => !string.IsNullOrEmpty(x.Website));

        RuleFor(x => x.LogoUrl)
            .NotEmpty()
            .MaximumLength(1000).Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .When(x => !string.IsNullOrEmpty(x.LogoUrl));
    }
}