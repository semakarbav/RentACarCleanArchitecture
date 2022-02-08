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

namespace Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarCommand: IRequest<Car>
    {
        public int Id { get; set; }
        public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Car>
        {
            ICarRepository _carRepository;
            IMapper _mapper;
            CarBusinessRules _carBusinessRules;

            public DeleteCarCommandHandler(ICarRepository carRepository, IMapper mapper, CarBusinessRules carBusinessRules)
            {
                _carRepository = carRepository;
                _mapper = mapper;
                _carBusinessRules = carBusinessRules;
            }

            public async Task<Car> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
            {
                //await _carBusinessRules.CheckIfCarIdShouldBeExist(request.Id);
                //await _carBusinessRules.CheckIfCarIsRented(request.Id);
                //await _carBusinessRules.CheckIfCarIsMaintenance(request.Id);
                    var mappedCar = _mapper.Map<Car>(request);
                    var deletedCar = await _carRepository.DeleteAsync(mappedCar);
                    return deletedCar;

            }
        }
    }
}
