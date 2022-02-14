using Application.Features.Cars.Commands.UpdateCarState;
using Application.Features.Cars.Rules;
using Application.Features.Maintenances.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Maintenenaces.Commands.CreateMaintenance
{
    public class CreateMaintenanceCommand : IRequest<Maintenance>
    {
        public string Description { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public int CarId { get; set; }

        public class CreateMaintenanceCommandHandler : IRequestHandler<CreateMaintenanceCommand, Maintenance>
        {
            IMaintenanceRepository _maintenanceRepository;
            IMapper _mapper;
            MaintenanceBusinessRules _maintenanceBusinessRules;
            CarBusinessRules _carBusinessRules;

            public CreateMaintenanceCommandHandler(IMaintenanceRepository maintenanceRepository, CarBusinessRules carBusinessRules ,IMapper mapper, MaintenanceBusinessRules maintenanceBusinessRules)
            {
                _maintenanceRepository = maintenanceRepository;
                _mapper = mapper;
                _maintenanceBusinessRules = maintenanceBusinessRules;
                _carBusinessRules = carBusinessRules;
            }

            public async Task<Maintenance> Handle(CreateMaintenanceCommand request, CancellationToken cancellationToken)
            {
                _maintenanceBusinessRules.CheckIfCarIsRented(request.CarId);

                var mappedMaintenance = _mapper.Map<Maintenance>(request);

                var createdMaintenance = await _maintenanceRepository.AddAsync(mappedMaintenance);

                UpdateCarStateCommand command = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Maintenance
                };

                await this._carBusinessRules.UpdateCarState(command);
                return createdMaintenance;
            }
        }
    }
}
