﻿using Application.Features.Cars.Commands.UpdateCarState;
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

namespace Application.Features.Maintenances.Commands.UpdateMaintenance
{
    public class UpdateMaintenanceCommand:IRequest<Maintenance>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int CarId { get; set; }

        public class UpdateMaintenanceCommandHandler : IRequestHandler<UpdateMaintenanceCommand, Maintenance>
        {
            IMaintenanceRepository _maintenanceRepository;
            IMapper _mapper;
            MaintenanceBusinessRules _maintenanceBusinessRules;

            public UpdateMaintenanceCommandHandler(IMaintenanceRepository maintenanceRepository, IMapper mapper, MaintenanceBusinessRules maintenanceBusinessRules)
            {
                _maintenanceRepository = maintenanceRepository;
                _mapper = mapper;
                _maintenanceBusinessRules = maintenanceBusinessRules;
            }

            public async Task<Maintenance> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
            {
                 _maintenanceBusinessRules.CheckIfCarIsRented(request.CarId);

                var mappedMaintenance = _mapper.Map<Maintenance>(request);

                var updatedMaintenance = await _maintenanceRepository.UpdateAsync(mappedMaintenance);
                UpdateCarStateCommand command = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Available
                };
                return updatedMaintenance;
            }
        }
    }

}
