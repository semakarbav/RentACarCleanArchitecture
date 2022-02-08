using Application.Features.Tranmissions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tranmissions.Commands.CreateTransmission
{
    public class CreateTransmissionCommand:IRequest<Transmission>
    {
        public string Name { get; set; }
       
        public class CreateTransmissionCommandHandler : IRequestHandler<CreateTransmissionCommand, Transmission>
        {
            ITransmissionRepository _transmissonRepository;
            IMapper _mapper;
            TransmissionBusinessRules _transmissionBusinessRules;

            public CreateTransmissionCommandHandler(IMapper mapper, ITransmissionRepository transmissonRepository, TransmissionBusinessRules transmissionBusinessRules)
            {
                _mapper = mapper;
                _transmissonRepository = transmissonRepository;
                _transmissionBusinessRules = transmissionBusinessRules;
            }

            public async Task<Transmission> Handle(CreateTransmissionCommand request, CancellationToken cancellationToken)
            {
                await _transmissionBusinessRules.TransmissionNameCanNotBeDuplicatedWhenInserted(request.Name);
                var mappedTransmission = _mapper.Map<Transmission>(request);
                var createdTransmission = await _transmissonRepository.AddAsync(mappedTransmission);
                return createdTransmission;
            }
        }
    }
}
