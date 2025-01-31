using CompanyRatingApi.Application.Companies.Dtos;
using CompanyRatingApi.Application.Companies.Enums;
using CompanyRatingApi.Application.Companies.Handlers.Comment.CommentAdd;
using CompanyRatingApi.Application.Companies.Handlers.CompanyAdd;
using CompanyRatingApi.Application.Companies.Handlers.CompanyDelete;
using CompanyRatingApi.Application.Companies.Handlers.CompanyGet;
using CompanyRatingApi.Application.Companies.Handlers.CompanyGetAll;
using CompanyRatingApi.Application.Companies.Handlers.CompanyUpdate;
using CompanyRatingApi.Application.Companies.Handlers.Rating.CompanyRating;
using CompanyRatingApi.Shared.Auth;
using CompanyRatingApi.Shared.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyRatingApi.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class CompanyController(
    CompanyGetHandler companyGet,
    CompanyGetAllHandler companyGetAll,
    CompanyAddHandler companyAdd,
    CompanyUpdateHandler companyUpdate,
    CompanyDeleteHandler companyDelete,
    CompanyRatingHandler companyRating,
    CommentAddHandler commentAdd
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedList<CompanyDto>>> Get(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string? sortBy = null,
        [FromQuery] SortDirection? sortDirection = null,
        [FromQuery] string? name = null,
        [FromQuery] IEnumerable<CompanyIndustry>? industries = null,
        CancellationToken cancellationToken = default
    )
    {
        var request = new CompanyGetAllRequest()
        {
            Page = page,
            Size = size,
            SortBy = sortBy,
            SortDirection = sortDirection,
            Name = name,
            Industries = industries
        };
        return Ok(await companyGetAll.Handle(request, cancellationToken));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<CompanyDetailDto>> Get(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await companyGet.Handle(new CompanyGetRequest(id), cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<CompanyDto>> Post(
        [FromBody] CompanyAddRequest request,
        CancellationToken cancellationToken
    )
    {
        var company = await companyAdd.Handle(request, cancellationToken);
        return CreatedAtAction("Get", new { id = company.Id }, company);
    }

    [HttpPut("{id:Guid}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<CompanyDto>> Put(
        Guid id,
        [FromBody] CompanyUpdateRequest request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await companyUpdate.Handle(request with { Id = id }, cancellationToken));
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<ActionResult<CompanyDto>> Delete(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await companyDelete.Handle(new CompanyDeleteRequest(id), cancellationToken));
    }

    [HttpPut("{id:Guid}/Rating")]
    public async Task<ActionResult<CompanyDto>> Rating(
        Guid id,
        [FromBody] CompanyRatingRequest request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await companyRating.Handle(request with { Id = id }, cancellationToken));
    }

    [HttpPut("{id:Guid}/Comment")]
    public async Task<ActionResult<CompanyCommentDto>> Post(
        Guid id,
        [FromBody] CommentAddRequest request,
        CancellationToken cancellationToken
    )
    {
        return Ok(await commentAdd.Handle(request with { CompanyId = id }, cancellationToken));
    }
}