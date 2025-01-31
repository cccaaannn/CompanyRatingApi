using Ardalis.GuardClauses;
using AutoMapper;
using CompanyRatingApi.Application.Companies.Dtos;
using CompanyRatingApi.Shared.Handlers;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Application.Companies.Handlers.CompanyGet;

public class CompanyGetHandler(
    ApplicationDbContext dbContext,
    IMapper mapper
) : IHandler<CompanyGetRequest, CompanyDetailDto>
{
    public async Task<CompanyDetailDto> Handle(CompanyGetRequest request, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
            .Include(c => c.Comments.OrderByDescending(comment => comment.CreatedAt))
            .ThenInclude(comment => comment.User)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        Guard.Against.Null(company, nameof(company));

        return mapper.Map<CompanyDetailDto>(company);
    }
}