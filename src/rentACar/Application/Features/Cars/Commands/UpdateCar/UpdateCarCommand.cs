using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarCommand: IRequest<Car>
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public int ModelId { get; set; }
        public string Plate { get; set; }
        public short ModelYear { get; set; }
        public class UpdateCarCommandHandler: IRequestHandler<UpdateCarCommand, Car>
        {
            ICarRepository _carRepository;
            IMapper _mapper;
            CarBusinessRules _carBusinessRules;

            public UpdateCarCommandHandler(CarBusinessRules carBusinessRules, IMapper mapper, ICarRepository carRepository)
            {
                _carBusinessRules = carBusinessRules;
                _mapper = mapper;
                _carRepository = carRepository;
            }

            public async Task<Car> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
            {
               // await _carBusinessRules.CheckIfCarIdShouldBeExist(request.Id);
                var mappedCar = _mapper.Map<Car>(request);
                var updatedCar = await _carRepository.UpdateAsync(mappedCar);
                return updatedCar;
            }
        }
    }
}
