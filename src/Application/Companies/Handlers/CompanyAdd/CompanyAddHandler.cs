using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompanyRateApi.Application.Companies.Dtos;
using CompanyRateApi.Application.Companies.Entities;
using CompanyRateApi.Shared.Exceptions;
using CompanyRateApi.Shared.Handlers;
using CompanyRateApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRateApi.Application.Companies.Handlers.CompanyAdd;

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