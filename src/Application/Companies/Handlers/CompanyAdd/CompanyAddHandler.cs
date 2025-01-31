using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompanyRatingApi.Application.Companies.Dtos;
using CompanyRatingApi.Application.Companies.Entities;
using CompanyRatingApi.Shared.Exceptions;
using CompanyRatingApi.Shared.Handlers;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Application.Companies.Handlers.CompanyAdd;

public class CompanyAddHandler(
    ApplicationDbContext dbContext,
    IMapper mapper
) : IHandler<CompanyAddRequest, CompanyDto>
{
    public async Task<CompanyDto> Handle(CompanyAddRequest request, CancellationToken cancellationToken)
    {
        var company = mapper.Map<Company>(request);

        await dbContext.Companies.AddAsync(company, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Companies
                   .ProjectTo<CompanyDto>(mapper.ConfigurationProvider)
                   .FirstOrDefaultAsync(x => x.Id == company.Id, cancellationToken)
               ?? throw new NotFoundException();
    }
}