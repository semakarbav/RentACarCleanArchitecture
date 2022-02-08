using Application.Features.Tranmissions.Commands.CreateTransmission;
using Application.Features.Tranmissions.Commands.DeleteTransmission;
using Application.Features.Tranmissions.Commands.UpdateTransmission;
using Application.Features.Tranmissions.Dtos;
using Application.Features.Tranmissions.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tranmissions.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Transmission, CreateTransmissionCommand>().ReverseMap();
            CreateMap<Transmission, UpdateTransmissionCommand>().ReverseMap();
            CreateMap<Transmission, DeleteTransmissionCommand>().ReverseMap();
            CreateMap<Transmission, TransmissionListDto>().ReverseMap();
            CreateMap<IPaginate<Transmission>, TransmissionListModel>().ReverseMap();
        }
    }
}
