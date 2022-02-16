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

namespace Application.Features.AdditionalServices.Commands.DeleteAdditionalService
{
    public class DeleteAdditionalServiceCommand: IRequest<DeletedAdditionalServiceDto>
    {
        public int Id { get; set; }
        public class DeleteAdditionalServiceCommandHandler : IRequestHandler<DeleteAdditionalServiceCommand, DeletedAdditionalServiceDto>
        {
            IAdditionalServiceRepository _additionalServiceRepository;
            IMapper _mapper;
            AdditionalServiceBusinessRules _additionalServiceBusinessRules;

            public DeleteAdditionalServiceCommandHandler(IAdditionalServiceRepository additionalServiceRepository, IMapper mapper, AdditionalServiceBusinessRules additionalServiceBusinessRules)
            {
                _additionalServiceRepository = additionalServiceRepository;
                _mapper = mapper;
                _additionalServiceBusinessRules = additionalServiceBusinessRules;
            }

            public async Task<DeletedAdditionalServiceDto> Handle(DeleteAdditionalServiceCommand request, CancellationToken cancellationToken)
            {
                await _additionalServiceBusinessRules.CheckIfAdditionalServiceIsExists(request.Id);
                var mappedAdditionalService = _mapper.Map<AdditionalService>(request);
                var deletedAdditionalService = await _additionalServiceRepository.DeleteAsync(mappedAdditionalService);
                var deletedAdditionalServiceDto = _mapper.Map<DeletedAdditionalServiceDto>(deletedAdditionalService);
                return deletedAdditionalServiceDto;
            }
        }
    }
}
