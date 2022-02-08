using Application.Features.Cars.Commands.UpdateCarState;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Rules
{
    public class CarBusinessRules
    {
        ICarRepository _carRepository;
         IMediator _mediator;

        public CarBusinessRules(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<Car> CheckIfCarIdShouldBeExist(int carId)
        {
            var result = await _carRepository.GetAsync(c => c.Id == carId);
            if (result == null)
            {
                throw new BusinessException("Araba bulunamadı");
            }
            return result;
           
        }
        public async Task CheckIfCarIsRented(int carId)
        {
            var car = await _carRepository.GetAsync(c => c.Id == carId);
            if (car.CarState == CarState.Rented)
            {
                throw new BusinessException("Araba Kirada");
            }
        }
        public async Task CheckIfCarIsMaintenance(int carId)
        {
            var car = await _carRepository.GetAsync(c => c.Id == carId);
            if (car.CarState == CarState.Maintenance)
            {
                throw new BusinessException("Araba Bakımda");
            }
        }
        public async Task CheckIfPlateCanNotBeDuplicated(string plate)
        {
            var result = await _carRepository.GetListAsync(c => c.Plate==plate);
            if (result.Items.Any())
            {
                throw new BusinessException("Plate exists");
            }
        }
        public async Task<int> GetFindexScoreById(int id)
        {
            var result = await _carRepository.GetAsync(c => c.Id == id);
            if (result is null)
            {
                throw new BusinessException("Araba Bulunamadı");
            }
            return result.FindexScore;
        }
        public async Task UpdateCarState(UpdateCarStateCommand updateCarStateCommand)
        {
            var result = await _mediator.Send(updateCarStateCommand);

            if (result == null)
            {
                throw new BusinessException("Araba durumu güncellenirken hata");
            }

        }
    }
}
