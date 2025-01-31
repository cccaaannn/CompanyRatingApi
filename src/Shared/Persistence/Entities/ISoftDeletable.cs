namespace CompanyRatingApi.Shared.Persistence.Entities;

public interface ISoftDeletable
{
    public bool IsDeleted { get; set; }
}