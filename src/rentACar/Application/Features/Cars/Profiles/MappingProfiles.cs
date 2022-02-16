using Application.Features.Cars.Commands;
using Application.Features.Cars.Commands.CreateCar;
using Application.Features.Cars.Commands.DeleteCar;
using Application.Features.Cars.Commands.EndRentalCarInfo;
using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.Cars.Commands.UpdateCarState;
using Application.Features.Cars.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Models
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Car, CreateCarCommand>().ReverseMap();
            CreateMap<Car, UpdateCarCommand>().ReverseMap();
            CreateMap<Car, DeleteCarCommand>().ReverseMap();
            CreateMap<Car, CarListDto>().ReverseMap();
            CreateMap<Car, UpdateCarDto>().ReverseMap();
            CreateMap<Car, EndRentalCarInfoCommand>().ReverseMap();
            CreateMap<Car, UpdateCarStateCommand>().ReverseMap();
            CreateMap<IPaginate<Car>,CarListModel>().ReverseMap();
        }
    }
}
