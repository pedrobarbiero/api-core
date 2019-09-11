using System;
using AutoMapper;
using Business.Models;
using WebApi.DataTransferObjects;

namespace WebApi.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Provider, ProviderDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProviderName,
                           opt => opt.MapFrom(src => src.Provider.Name));
        }
    }
}
