using FluentValidation;

namespace CompanyRatingApi.Application.Companies.Handlers.CompanyDelete;

public class CompanyDeleteRequestValidator : AbstractValidator<CompanyDeleteRequest>
{
    public CompanyDeleteRequestValidator()
    {
    }
}