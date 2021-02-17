using AutoMapper;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users,User>();
        }
    }
}
