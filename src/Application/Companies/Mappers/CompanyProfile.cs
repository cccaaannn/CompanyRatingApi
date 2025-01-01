using AutoMapper;
using CompanyRateApi.Application.Companies.Dtos;
using CompanyRateApi.Application.Companies.Entities;
using CompanyRateApi.Application.Companies.Handlers.CompanyAdd;
using CompanyRateApi.Application.Companies.Handlers.CompanyUpdate;

namespace CompanyRateApi.Application.Companies.Mappers;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyAddRequest, Company>().ReverseMap();

        CreateMap<CompanyUpdateRequest, Company>().ReverseMap();

        CreateMap<CompanyDto, Company>().ReverseMap();
    }
}