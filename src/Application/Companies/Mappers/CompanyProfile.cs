using AutoMapper;
using CompanyRatingApi.Application.Companies.Dtos;
using CompanyRatingApi.Application.Companies.Entities;
using CompanyRatingApi.Application.Companies.Handlers.CompanyAdd;
using CompanyRatingApi.Application.Companies.Handlers.CompanyUpdate;

namespace CompanyRatingApi.Application.Companies.Mappers;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyAddRequest, Company>();

        CreateMap<CompanyUpdateRequest, Company>();

        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.RatingCount,
                opt =>
                    opt.MapFrom(src =>
                        src.Ratings.Count
                    )
            ).ReverseMap();

        CreateMap<Company, CompanyDetailDto>()
            .IncludeBase<Company, CompanyDto>();

        CreateMap<CompanyComment, CompanyCommentDto>()
            .ForMember(dest => dest.UserName,
                opt =>
                    opt.MapFrom(src =>
                        src.User!.Name
                    )
            )
            .ForMember(dest => dest.UserSurname,
                opt =>
                    opt.MapFrom(src =>
                        src.User!.Surname
                    )
            ).ReverseMap();
    }
}