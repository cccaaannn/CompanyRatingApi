namespace CompanyRatingApi.Application.Companies.Dtos;

public record CompanyDetailDto : CompanyDto
{
    public IEnumerable<CompanyCommentDto> Comments { get; init; } = [];
}