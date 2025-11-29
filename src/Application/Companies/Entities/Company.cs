using System.ComponentModel.DataAnnotations;
using CompanyRatingApi.Application.Companies.Enums;
using CompanyRatingApi.Shared.Persistence.Entities;

namespace CompanyRatingApi.Application.Companies.Entities;

public class Company : EntityBase, ISoftDeletable
{
    [MaxLength(1000)] public string Name { get; set; } = string.Empty;

    public CompanyIndustry Industry { get; set; } = CompanyIndustry.Other;

    [MaxLength(5000)] public string Description { get; set; } = string.Empty;

    [MaxLength(1000)] public string Address { get; set; } = string.Empty;

    [MaxLength(500)] public string City { get; set; } = string.Empty;

    [MaxLength(500)] public string Country { get; set; } = string.Empty;

    [MaxLength(1000)] public string Email { get; set; } = string.Empty;

    [MaxLength(1000)] public string Website { get; set; } = string.Empty;

    [MaxLength(1000)] public string LogoUrl { get; set; } = string.Empty;

    public double AverageRating { get; set; } = 0.0;

    public virtual ICollection<CompanyRating> Ratings { get; set; } = [];

    public virtual ICollection<CompanyComment> Comments { get; set; } = [];
    
    public bool IsDeleted { get; set; } = false;
}