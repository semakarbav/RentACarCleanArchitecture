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

namespace Application.Features.Tranmissions.Commands.UpdateTransmission
{
    public class UpdateTransmissionCommand : IRequest<Transmission>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class CreateTransmissionCommandHandler : IRequestHandler<UpdateTransmissionCommand, Transmission>
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

            public async Task<Transmission> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
            {
                await _transmissionBusinessRules.TransmissionNameCanNotBeDuplicatedWhenInserted(request.Name);
                var mappedTransmission = _mapper.Map<Transmission>(request);
                var updatedTransmission = await _transmissonRepository.UpdateAsync(mappedTransmission);
                return updatedTransmission;
            }
        }
    }
}
