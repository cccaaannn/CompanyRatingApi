using System.Text.Json.Serialization;
using CompanyRatingApi.Application.Companies.Enums;

namespace CompanyRatingApi.Application.Companies.Handlers.Comment.CommentAdd;

public record CommentAddRequest
{
    [JsonIgnore] public Guid CompanyId { get; init; }

    public required string Content { get; init; } = string.Empty;
}