using Application.Features.Cars.Rules;
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

namespace Application.Features.Cars.Commands.CreateCar

{
    public class CreateCarCommand: IRequest<Car>
    {
        public int ColorId { get; set; }
        public int ModelId { get; set; }
        public int CityId { get; set; }
        public string Plate { get; set; }
        public short ModelYear { get; set; }
        public int FindexScore { get; set; }
        public int Kilometer { get; set; }


        public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Car>
        {
            ICarRepository _carRepository;
            IMapper _mapper;
            CarBusinessRules _carBusinessRules;

            public CreateCarCommandHandler(CarBusinessRules carBusinessRules, ICarRepository carRepository, IMapper mapper)
            {
                _carBusinessRules = carBusinessRules;
                _carRepository = carRepository;
                _mapper = mapper;
            }

            public async Task<Car> Handle(CreateCarCommand request, CancellationToken cancellationToken)
            {
                await _carBusinessRules.CheckIfPlateCanNotBeDuplicated(request.Plate);
                var mappedCar = _mapper.Map<Car>(request);
                mappedCar.CarState = CarState.Available;
                var createdCar = await _carRepository.AddAsync(mappedCar);
                return createdCar;
            }
        }
    }
}
