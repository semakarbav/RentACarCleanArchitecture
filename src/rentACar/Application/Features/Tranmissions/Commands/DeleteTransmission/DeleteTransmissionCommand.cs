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

namespace Application.Features.Tranmissions.Commands.DeleteTransmission
{
    public class DeleteTransmissionCommand: IRequest<Transmission>
    {
        public int Id { get; set; }

        public class DeleteTransmissionCommandHandler : IRequestHandler<DeleteTransmissionCommand, Transmission>
        {
            ITransmissionRepository _transmissionRepository;
            IMapper _mapper;
           
            public DeleteTransmissionCommandHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
            {
                _transmissionRepository = transmissionRepository;
                _mapper = mapper;
            }

            public async Task<Transmission> Handle(DeleteTransmissionCommand request, CancellationToken cancellationToken)
            {
                var mappedTransmission = _mapper.Map<Transmission>(request);
                var deletedTransmission = await _transmissionRepository.DeleteAsync(mappedTransmission);
                return deletedTransmission;
            }
        }
    }
}
