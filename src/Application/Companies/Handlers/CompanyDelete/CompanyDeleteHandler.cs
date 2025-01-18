using AutoMapper;
using CompanyRateApi.Application.Companies.Dtos;
using CompanyRateApi.Shared.Exceptions;
using CompanyRateApi.Shared.Handlers;
using CompanyRateApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRateApi.Application.Companies.Handlers.CompanyDelete;

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