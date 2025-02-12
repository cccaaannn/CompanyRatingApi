using AutoMapper;
using CompanyRatingApi.Application.Companies.Dtos;
using CompanyRatingApi.Application.Companies.Entities;

namespace CompanyRatingApi.Application.Companies.Mappers;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CompanyComment, CompanyCommentDto>();
    }
}