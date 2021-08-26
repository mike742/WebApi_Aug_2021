using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Aug_2021.DTOs;
using WebApi_Aug_2021.Models;

namespace WebApi_Aug_2021.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderCreateDto>();
            CreateMap<OrderCreateDto, Order>();

            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderReadDto, Order>();
        }
    }
}
