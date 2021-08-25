using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Aug_2021.DTOs;
using WebApi_Aug_2021.Models;

namespace WebApi_Aug_2021.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductCreateDto>();
            CreateMap<ProductCreateDto, Product>();
        }
    }
}
