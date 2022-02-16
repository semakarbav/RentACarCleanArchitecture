using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Queries.GetAdditionalServiceById
{
    public class GetAdditionalServiceByIdQuery : IRequest<AdditionalServiceDto>
    {
        public int Id { get; set; }
        public class GetAdditionalServiceByIdQueryHandler : IRequestHandler<GetAdditionalServiceByIdQuery, AdditionalServiceDto>
        {
            IAdditionalServiceRepository _additionalServiceRepository;
            IMapper _mapper;
            AdditionalServiceBusinessRules _additionalServiceBusinessRules;

            public GetAdditionalServiceByIdQueryHandler(IAdditionalServiceRepository additionalServiceRepository, IMapper mapper, AdditionalServiceBusinessRules additionalServiceBusinessRules)
            {
                _additionalServiceRepository = additionalServiceRepository;
                _mapper = mapper;
                _additionalServiceBusinessRules = additionalServiceBusinessRules;
            }

            public async Task<AdditionalServiceDto> Handle(GetAdditionalServiceByIdQuery request, CancellationToken cancellationToken)
            {
                await _additionalServiceBusinessRules.CheckIfAdditionalServiceIsExists(request.Id);

                var additionalService = await _additionalServiceRepository.GetAsync(a => a.Id == request.Id);
                var additionalServiceDto = _mapper.Map<AdditionalServiceDto>(additionalService);
                return additionalServiceDto;
            }
        }
    }
}
