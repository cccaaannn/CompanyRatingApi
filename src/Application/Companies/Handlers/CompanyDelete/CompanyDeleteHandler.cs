using AutoMapper;
using CompanyRatingApi.Application.Companies.Dtos;
using CompanyRatingApi.Shared.Exceptions;
using CompanyRatingApi.Shared.Handlers;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Application.Companies.Handlers.CompanyDelete;

public class CompanyDeleteHandler(
    ApplicationDbContext dbContext,
    IMapper mapper
) : IHandler<CompanyDeleteRequest, CompanyDto>
{
    public async Task<CompanyDto> Handle(CompanyDeleteRequest request, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
                          .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                      ?? throw new NotFoundException();

        dbContext.Companies.Remove(company);

        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<CompanyDto>(company);
    }
}