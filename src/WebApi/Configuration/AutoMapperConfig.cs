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
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
