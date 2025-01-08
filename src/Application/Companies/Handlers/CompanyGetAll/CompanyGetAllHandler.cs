using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompanyRateApi.Application.Companies.Dtos;
using CompanyRateApi.Application.Companies.Entities;
using CompanyRateApi.Shared.Handlers;
using CompanyRateApi.Shared.Pagination;
using CompanyRateApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRateApi.Application.Companies.Handlers.CompanyGetAll;

public class CompanyGetAllHandler(
    ApplicationDbContext dbContext,
    IMapper mapper
) : IHandler<CompanyGetAllRequest, PagedList<CompanyDto>>
{
    public async Task<PagedList<CompanyDto>> Handle(CompanyGetAllRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Company> query = dbContext.Companies;

        var sortBy = GetOrderField(request.SortBy);
        query = request.SortDirection == SortDirection.Ascending
            ? query.OrderBy(sortBy)
            : query.OrderByDescending(sortBy);

        return await query
            .ProjectTo<CompanyDto>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request.Page, request.Size, cancellationToken);
    }

    private static Expression<Func<Company, object>> GetOrderField(string? sortBy)
    {
        if (sortBy == null)
            return x => x.CreatedAt;

        var property = typeof(Company).GetProperty(sortBy,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
        );

        if (property != null)
            return x => EF.Property<object>(x, sortBy);

        return x => x.CreatedAt;
    }
}