using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Commands.UpdateAdditionalService
{
    public class UpdateAdditionalServiceCommand: IRequest<UpdatedAdditionalServiceDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public class UpdateAdditionalServiceCommandHandler : IRequestHandler<UpdateAdditionalServiceCommand, UpdatedAdditionalServiceDto>
        {
            IAdditionalServiceRepository _additionalServiceRepository;
            IMapper _mapper;
            AdditionalServiceBusinessRules _additionalServiceBusinessRules;

            public UpdateAdditionalServiceCommandHandler(IAdditionalServiceRepository additionalServiceRepository, IMapper mapper, AdditionalServiceBusinessRules additionalServiceBusinessRules)
            {
                _additionalServiceRepository = additionalServiceRepository;
                _mapper = mapper;
                _additionalServiceBusinessRules = additionalServiceBusinessRules;
            }
            public async Task<UpdatedAdditionalServiceDto> Handle(UpdateAdditionalServiceCommand request, CancellationToken cancellationToken)
            {
                var mappedAdditionalService = _mapper.Map<AdditionalService>(request);
                var updatedAdditionalService = await _additionalServiceRepository.UpdateAsync(mappedAdditionalService);
                var updatedAdditionalServiceDto = _mapper.Map<UpdatedAdditionalServiceDto>(updatedAdditionalService);
                return updatedAdditionalServiceDto;
            }
        }
    }
}
