using Application.Features.Cars.Commands.EndRentalCarInfo;
using Application.Features.Cars.Commands.UpdateCarState;
using Application.Features.Cars.Rules;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Rentals.Dtos;
using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.CreateRental.CreateEndRentalForIndividualCustomer
{
    public class CreateEndRentalCommandForIndividualCommand: IRequest<RentalListDto>
    {
        public int Id { get; set; }
        public DateTime ReturnedDate { get; set; }
        public int ReturnedKilometer { get; set; }
        public int ReturnedCityId { get; set; }
        public int CarId { get; set; }

        public class CreateEndRentalCommandForCorporateCommandHandler : IRequestHandler<CreateEndRentalCommandForIndividualCommand, RentalListDto>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            IndividualCustomerBusinessRules _corporateCustomerBusinessRules;
            CarBusinessRules _carBusinessRules;

            public CreateEndRentalCommandForCorporateCommandHandler(IRentalRepository rentalRepository, IMapper mapper, RentalBusinessRules rentalBusinessRules, IndividualCustomerBusinessRules corporateCustomerBusinessRules, CarBusinessRules carBusinessRules)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
                _carBusinessRules = carBusinessRules;
            }

            public async Task<RentalListDto> Handle(CreateEndRentalCommandForIndividualCommand request, CancellationToken cancellationToken)
            {
                await this._rentalBusinessRules.CheckIfKilometerControls(request.CarId, request.ReturnedKilometer);
                var rentalEnd = await _rentalRepository.GetAsync(r => r.Id == request.Id);
                var mappedRental = _mapper.Map(request, rentalEnd);

                var createdRental = await _rentalRepository.UpdateAsync(mappedRental);
                UpdateCarStateCommand updateCarState = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Available
                };
                await _carBusinessRules.UpdateCarState(updateCarState);

                EndRentalCarInfoCommand endRentalCarInfo = new EndRentalCarInfoCommand
                {
                    Id = request.CarId,
                    Kilometer = request.ReturnedKilometer,
                    CityId = request.ReturnedCityId
                };
                await _carBusinessRules.UpdateCarKilometerCityInfo(endRentalCarInfo);
                var createdRentalDto = _mapper.Map<RentalListDto>(createdRental);
                return createdRentalDto;
            }
        }
    }
    
}
