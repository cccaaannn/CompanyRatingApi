using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CompanyRatingApi.Application.Companies.Dtos;
using CompanyRatingApi.Application.Companies.Entities;
using CompanyRatingApi.Shared.Context;
using CompanyRatingApi.Shared.Handlers;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Application.Companies.Handlers.Comment.CommentAdd;

public class CommentAddHandler(
    ApplicationDbContext dbContext,
    CurrentUser currentUser,
    IMapper mapper
) : IHandler<CommentAddRequest, CompanyCommentDto>
{
    public async Task<CompanyCommentDto> Handle(CommentAddRequest request, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies
                          .Include(x => x.Comments)
                          .FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);

        Guard.Against.Null(company, nameof(company));

        var comment = new CompanyComment()
        {
            CompanyId = request.CompanyId,
            AppUserId = currentUser.Id,
            Content = request.Content
        };

        company.Comments.Add(comment);

        await dbContext.SaveChangesAsync(cancellationToken);

        var savedComment = await dbContext.CompanyComments
            .ProjectTo<CompanyCommentDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == comment.Id, cancellationToken);

        Guard.Against.Null(savedComment, nameof(savedComment));

        return savedComment;
    }
}