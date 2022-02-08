using Application.Features.Fuels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Commands.DeleteFuel
{
    public class DeleteFuelCommand:IRequest<Fuel>
    {
        public int Id { get; set; }
        public class DeleteFuelCommandHandler : IRequestHandler<DeleteFuelCommand, Fuel>
        {
            IFuelRepository _fuelRepository;
            IMapper _mapper;
            FuelBusinessRules _fuelBusinessRules;

            public DeleteFuelCommandHandler(IFuelRepository fuelRepository, IMapper mapper, FuelBusinessRules fuelBusinessRules)
            {
                _fuelRepository = fuelRepository;
                _mapper = mapper;
                _fuelBusinessRules = fuelBusinessRules;
            }

            public async Task<Fuel> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
            {
                var mappedFuel = _mapper.Map<Fuel>(request);
                var deletedFuel = await _fuelRepository.DeleteAsync(mappedFuel);
                return deletedFuel;
            }
        }
    }
}
