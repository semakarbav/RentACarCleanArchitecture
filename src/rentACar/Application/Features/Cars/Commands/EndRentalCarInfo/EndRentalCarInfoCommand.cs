using Application.Features.Cars.Dtos;
using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.EndRentalCarInfo
{
    public class EndRentalCarInfoCommand : IRequest<UpdateCarDto>
    {
        public int Id { get; set; }
        public int Kilometer { get; set; }
        public int CityId { get; set; }

        public class EndRentalCarInfoCommandHandler : IRequestHandler<EndRentalCarInfoCommand, UpdateCarDto>
        {
            ICarRepository _carRepository;
            IMapper _mapper;
            CarBusinessRules _carBusinessRules;

            public EndRentalCarInfoCommandHandler(ICarRepository carRepository,
                 CarBusinessRules carBusinessRules, IMapper mapper)
            {
                _carRepository = carRepository;
                _carBusinessRules = carBusinessRules;
                _mapper = mapper;
            }

            public async Task<UpdateCarDto> Handle(EndRentalCarInfoCommand request, CancellationToken cancellationToken)
            {
                var carToUpdate = await _carRepository.GetAsync(c => c.Id == request.Id);
                carToUpdate = this._mapper.Map(request, carToUpdate);

                var updatedCar = await _carRepository.UpdateAsync(carToUpdate);
                var carToReturn = _mapper.Map<UpdateCarDto>(updatedCar);
                return carToReturn;
            }
        }
    }
    
}
