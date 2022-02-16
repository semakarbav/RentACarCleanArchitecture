using Application.Features.CarDamages.Commands.CreateCarDamage;
using Application.Features.CarDamages.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CarDamages.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CarDamage, CreateCarDamageCommand>().ReverseMap();
            CreateMap<CarDamage, CarDamageDto>().ReverseMap();
          
        }
    }
}
