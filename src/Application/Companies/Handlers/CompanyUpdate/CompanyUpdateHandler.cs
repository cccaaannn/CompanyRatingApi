using AutoMapper;
using CompanyRatingApi.Application.Companies.Dtos;
using CompanyRatingApi.Shared.Exceptions;
using CompanyRatingApi.Shared.Handlers;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Application.Companies.Handlers.CompanyUpdate;

public class CompanyUpdateHandler(
    ApplicationDbContext dbContext,
    IMapper mapper
) : IHandler<CompanyUpdateRequest, CompanyDto>
{
    public async Task<CompanyDto> Handle(CompanyUpdateRequest request, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
                          .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                      ?? throw new NotFoundException();

        company = mapper.Map(request, company);

        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<CompanyDto>(company);
    }
}