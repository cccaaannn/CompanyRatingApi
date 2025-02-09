using System.Text.Json.Serialization;

namespace CompanyRatingApi.Application.Companies.Handlers.Rating.CompanyRating;

public record CompanyRatingRequest
{
    [JsonIgnore] public Guid Id { get; init; }

    public int Rating { get; init; }
}