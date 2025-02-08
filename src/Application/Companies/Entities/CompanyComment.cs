using System.ComponentModel.DataAnnotations;
using CompanyRatingApi.Application.Authentication.Entities;
using CompanyRatingApi.Shared.Persistence.Entities;

namespace CompanyRatingApi.Application.Companies.Entities;

public class CompanyComment : EntityBase, ISoftDeletable
{
    public Guid CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public Guid AppUserId { get; set; }

    public virtual AppUser? User { get; set; }

    [MaxLength(5000)] public string Content { get; set; } = string.Empty;

    public bool IsDeleted { get; set; } = false;
}