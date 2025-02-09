using CompanyRatingApi.Application.Authentication.Entities;
using CompanyRatingApi.Shared.Persistence.Entities;

namespace CompanyRatingApi.Application.Companies.Entities;

public class CompanyRating : EntityBase
{
    public Guid CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public Guid AppUserId { get; set; }

    public virtual AppUser? User { get; set; }

    public int Rating { get; set; }
}