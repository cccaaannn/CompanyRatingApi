using AutoMapper;
using CompanyRatingApi.Application.Authentication.Entities;
using CompanyRatingApi.Application.Authentication.Handlers.Register;

namespace CompanyRatingApi.Application.Authentication.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequest, AppUser>().ReverseMap();
    }
}