using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Maintenances.Commands.DeleteMaintenance;
using Application.Features.Maintenances.Commands.UpdateMaintenance;
using Application.Features.Maintenances.Dtos;
using Application.Features.Maintenances.Models;
using Application.Features.Maintenenaces.Commands.CreateMaintenance;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenances.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Maintenance, CreateMaintenanceCommand>().ReverseMap();
            CreateMap<Maintenance, UpdateMaintenanceCommand>().ReverseMap();
            CreateMap<Maintenance, DeleteMaintenanceCommand>().ReverseMap();
            CreateMap<Maintenance, MaintenanceListDto>().ReverseMap();
            CreateMap<IPaginate<Maintenance>, MaintenanceListModel>().ReverseMap();
        }
    }
}
