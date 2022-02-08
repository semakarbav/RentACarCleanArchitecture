using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenances.Commands.DeleteMaintenance
{
    public class DeleteMaintenanceCommand:IRequest<Maintenance>
    {
        public int Id { get; set; }
        public class DeleteBrandCommandHandler : IRequestHandler<DeleteMaintenanceCommand, Maintenance>
        {
            IMaintenanceRepository _maintenanceRepository;
            IMapper _mapper;

            public DeleteBrandCommandHandler(IMaintenanceRepository maintenanceRepository, IMapper mapper)
            {
                _maintenanceRepository = maintenanceRepository;
                _mapper = mapper;
            }

            public async Task<Maintenance> Handle(DeleteMaintenanceCommand request, CancellationToken cancellationToken)
            {
                var mappedMaintenance =  _mapper.Map<Maintenance>(request);
                var deletedMaintenance = await _maintenanceRepository.DeleteAsync(mappedMaintenance);
                return deletedMaintenance;
            }
        }
    }
}
