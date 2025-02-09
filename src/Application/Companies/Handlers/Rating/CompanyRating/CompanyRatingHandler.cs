using System.Data;
using Ardalis.GuardClauses;
using AutoMapper;
using CompanyRatingApi.Application.Companies.Dtos;
using CompanyRatingApi.Application.Companies.Entities;
using CompanyRatingApi.Shared.Context;
using CompanyRatingApi.Shared.Handlers;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Application.Companies.Handlers.Rating.CompanyRating;

public class CompanyRatingHandler(
    ILogger<CompanyRatingHandler> logger,
    ApplicationDbContext dbContext,
    CurrentUser currentUser,
    IMapper mapper
) : IHandler<CompanyRatingRequest, CompanyDto>
{
    public async Task<CompanyDto> Handle(CompanyRatingRequest request, CancellationToken cancellationToken)
    {
        await using var transaction =
            await dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

        try
        {
            var company = await dbContext.Companies
                .Include(x => x.Ratings)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            Guard.Against.Null(company, nameof(company));

            AddUserRating(company, request);

            RecalculateAverageRating(company);

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return mapper.Map<CompanyDto>(company);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while rating company, rolling back transaction");
            await transaction.RollbackAsync(cancellationToken);
            throw new Exception("Error while rating company");
        }
    }

    private void AddUserRating(Company company, CompanyRatingRequest request)
    {
        var userRating = company.Ratings.FirstOrDefault(x => x.AppUserId == currentUser.Id);

        if (userRating is null)
        {
            var newRating = new Entities.CompanyRating()
            {
                CompanyId = request.Id,
                AppUserId = currentUser.Id,
                Rating = request.Rating
            };

            company.Ratings.Add(newRating);
        }
        else
        {
            userRating.Rating = request.Rating;
        }
    }

    private void RecalculateAverageRating(Company company)
    {
        var averageRating = company.Ratings.Average(x => x.Rating);

        company.AverageRating = averageRating;
    }
}