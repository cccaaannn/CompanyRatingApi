using CompanyRateApi.Application.Companies.Dtos;
using CompanyRateApi.Application.Companies.Handlers.CompanyAdd;
using CompanyRateApi.Application.Companies.Handlers.CompanyDelete;
using CompanyRateApi.Application.Companies.Handlers.CompanyGet;
using CompanyRateApi.Application.Companies.Handlers.CompanyGetAll;
using CompanyRateApi.Application.Companies.Handlers.CompanyUpdate;
using CompanyRateApi.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CompanyRateApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController(
    CompanyGetHandler companyGet,
    CompanyGetAllHandler companyGetAll,
    CompanyAddHandler companyAdd,
    CompanyUpdateHandler companyUpdate,
    CompanyDeleteHandler companyDelete
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<CompanyDto>>> Get(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string? sortBy = null,
        [FromQuery] SortDirection? sortDirection = null,
        CancellationToken cancellationToken = default
    )
    {
        var request = new CompanyGetAllRequest()
        {
            Page = page,
            Size = size,
            SortBy = sortBy,
            SortDirection = sortDirection
        };
        return Ok(await companyGetAll.Handle(request, cancellationToken));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<CompanyDto>> Get(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await companyGet.Handle(new CompanyGetRequest(id), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> Post(
        [FromBody] CompanyAddRequest request,
        CancellationToken cancellationToken
    )
    {
        var company = await companyAdd.Handle(request, cancellationToken);
        return CreatedAtAction("Get", new { id = company.Id }, company);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<CompanyDto>> Put(
        Guid id,
        [FromBody] CompanyUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await companyUpdate.Handle(request with { Id = id }, cancellationToken));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<CompanyDto>> Delete(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await companyDelete.Handle(new CompanyDeleteRequest(id), cancellationToken));
    }
}