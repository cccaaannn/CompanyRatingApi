using System.ComponentModel.DataAnnotations;
using CompanyRateApi.Application.Companies.Enums;
using CompanyRateApi.Shared.Persistence.Entities;

namespace CompanyRateApi.Application.Companies.Entities;

public class Company : EntityBase, ISoftDeletable
{
    [MaxLength(100)] public string Name { get; set; } = string.Empty;

    public CompanyIndustry Industry { get; set; } = CompanyIndustry.Other;

    [MaxLength(500)] public string Description { get; set; } = string.Empty;

    [MaxLength(500)] public string Address { get; set; } = string.Empty;

    [MaxLength(100)] public string City { get; set; } = string.Empty;

    [MaxLength(100)] public string Country { get; set; } = string.Empty;

    [MaxLength(100)] public string Email { get; set; } = string.Empty;

    [MaxLength(100)] public string Website { get; set; } = string.Empty;

    [MaxLength(100)] public string LogoUrl { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }
}