using AutoMapper;
using RepositoryPattern.Application.Dtos;
using RepositoryPattern.Domain.Entities;

namespace RepositoryPattern.Application.Mappings;

public class GeneralMappings : Profile
{
    public GeneralMappings()
    {
        CreateMap<Product, ProductCreateRequest>().ReverseMap();
        CreateMap<Product, GetAllProductsResponse>().ReverseMap();
        CreateMap<Product, GetProductByIdResponse>().ReverseMap();
    }
}
