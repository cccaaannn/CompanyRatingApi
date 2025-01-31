using System.ComponentModel.DataAnnotations;
using CompanyRatingApi.Shared.Auth;
using CompanyRatingApi.Shared.Persistence.Entities;

namespace CompanyRatingApi.Application.Authentication.Entities;

public class AppUser : EntityBase, ISoftDeletable
{
    [EmailAddress] [MaxLength(200)] public string Email { get; set; } = string.Empty;

    [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [MaxLength(100)] public string Surname { get; set; } = string.Empty;

    [MaxLength(1000)] public string Password { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.User;

    public bool IsDeleted { get; set; }
}