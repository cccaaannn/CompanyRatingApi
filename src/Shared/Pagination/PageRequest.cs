namespace CompanyRatingApi.Shared.Pagination;

public record PageRequest
{
    public int Page { get; init; } = 1;
    public int Size { get; init; } = 10;
    public string? SortBy { get; init; }
    public SortDirection? SortDirection { get; init; }
}