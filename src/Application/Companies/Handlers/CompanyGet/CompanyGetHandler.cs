using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompanyRateApi.Application.Companies.Dtos;
using CompanyRateApi.Shared.Exceptions;
using CompanyRateApi.Shared.Handlers;
using CompanyRateApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRateApi.Application.Companies.Handlers.CompanyGet;

public class CompanyGetHandler(
    ApplicationDbContext dbContext,
    IMapper mapper
) : IHandler<CompanyGetRequest, CompanyDto>
{
    public async Task<CompanyDto> Handle(CompanyGetRequest request, CancellationToken cancellationToken)
    {
        return await dbContext.Companies
                   .ProjectTo<CompanyDto>(mapper.ConfigurationProvider)
                   .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
               ?? throw new NotFoundException();
    }
}