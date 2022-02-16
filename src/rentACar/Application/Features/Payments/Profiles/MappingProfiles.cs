using Application.Features.Payments.Commands.CreatePayment;
using Application.Features.Payments.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Payment, CreatePaymentCommand>().ReverseMap();
            CreateMap<Payment, CreatePaymentDto>().ReverseMap();
            
        }
    }
}
