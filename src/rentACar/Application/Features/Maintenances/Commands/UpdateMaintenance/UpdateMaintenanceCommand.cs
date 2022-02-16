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
            CarBusinessRules _carBusinessRules;

            public UpdateMaintenanceCommandHandler(IMaintenanceRepository maintenanceRepository, IMapper mapper, MaintenanceBusinessRules maintenanceBusinessRules, CarBusinessRules carBusinessRules)
            {
                _maintenanceRepository = maintenanceRepository;
                _mapper = mapper;
                _maintenanceBusinessRules = maintenanceBusinessRules;
                _carBusinessRules = carBusinessRules;
            }

            public async Task<Maintenance> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
            {
                 _maintenanceBusinessRules.CheckIfCarIsRented(request.CarId);

                var mappedMaintenance = _mapper.Map<Maintenance>(request);

                var updatedMaintenance = await _maintenanceRepository.UpdateAsync(mappedMaintenance);
                if(request.ReturnDate != null)
                {
                    UpdateCarStateCommand command = new UpdateCarStateCommand
                    {
                        Id = request.CarId,
                        CarState = CarState.Available
                    };
                    await this._carBusinessRules.UpdateCarState(command);
                   
                }
                return updatedMaintenance;
            }
        }
    }

}
