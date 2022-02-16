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

namespace Application.Features.AdditionalServices.Commands.CreateAdditionalService
{
    public class CreateAdditionalServiceCommand: IRequest<CreatedAdditionalServiceDto>
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public class CreateAdditionalServiceCommandHandler : IRequestHandler<CreateAdditionalServiceCommand,
            CreatedAdditionalServiceDto>
        {
             IAdditionalServiceRepository _additionalServiceRepository;
             IMapper _mapper;
             AdditionalServiceBusinessRules _additionalServiceBusinessRules;

            public CreateAdditionalServiceCommandHandler(IAdditionalServiceRepository additionalServiceRepository, IMapper mapper, AdditionalServiceBusinessRules additionalServiceBusinessRules)
            {
                _additionalServiceRepository = additionalServiceRepository;
                _mapper = mapper;
                _additionalServiceBusinessRules = additionalServiceBusinessRules;
            }

            public async Task<CreatedAdditionalServiceDto> Handle(CreateAdditionalServiceCommand request, CancellationToken cancellationToken)
            {
                await _additionalServiceBusinessRules.AdditionalServiceNameCanNotBeDuplicated(request.Name);

                var mappedAdditionalService = _mapper.Map<AdditionalService>(request);
                var createdAddtionalService = await _additionalServiceRepository.AddAsync(mappedAdditionalService);
                var createdAdditionalServiceDto = _mapper.Map<CreatedAdditionalServiceDto>(createdAddtionalService);

                return createdAdditionalServiceDto;
            }
        }
    }
}
