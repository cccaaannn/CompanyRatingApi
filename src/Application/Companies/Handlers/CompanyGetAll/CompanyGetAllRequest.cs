using CompanyRatingApi.Application.Companies.Enums;
using CompanyRatingApi.Shared.Pagination;

namespace CompanyRatingApi.Application.Companies.Handlers.CompanyGetAll;

public record CompanyGetAllRequest : PageRequest
{
    public string? Name { get; init; }
    public IEnumerable<CompanyIndustry>? Industries { get; init; }
}