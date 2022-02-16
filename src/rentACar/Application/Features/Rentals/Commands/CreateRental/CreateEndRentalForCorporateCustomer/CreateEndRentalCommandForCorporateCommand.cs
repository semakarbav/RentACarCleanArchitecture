using Application.Features.Cars.Commands.EndRentalCarInfo;
using Application.Features.Cars.Commands.UpdateCarState;
using Application.Features.Cars.Rules;
using Application.Features.CorporateCustomers.Rules;
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

namespace Application.Features.Rentals.Commands.CreateRental.CreateEndRentalForCorporateCustomer
{
    public class CreateEndRentalCommandForCorporateCommand: IRequest<RentalListDto>
    {
        public int Id { get; set; }
        public DateTime ReturnedDate { get; set; }
        public int ReturnedKilometer { get; set; }
        public int ReturnedCityId { get; set; }
        public int CarId { get; set; }

        public class CreateEndRentalCommandForCorporateCommandHandler: IRequestHandler<CreateEndRentalCommandForCorporateCommand, RentalListDto>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
            CarBusinessRules _carBusinessRules;

            public CreateEndRentalCommandForCorporateCommandHandler(IRentalRepository rentalRepository, IMapper mapper, RentalBusinessRules rentalBusinessRules, CorporateCustomerBusinessRules corporateCustomerBusinessRules, CarBusinessRules carBusinessRules)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
                _carBusinessRules = carBusinessRules;
            }

            public async Task<RentalListDto> Handle(CreateEndRentalCommandForCorporateCommand request, CancellationToken cancellationToken)
            {
                await this._rentalBusinessRules.CheckIfKilometerControls(request.CarId, request.ReturnedKilometer);
                var rentalEnd = await _rentalRepository.GetAsync(r => r.Id == request.Id);
                var mappedRental = _mapper.Map(request, rentalEnd);

                var createdRental = await _rentalRepository.UpdateAsync(mappedRental);
                UpdateCarStateCommand command = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Available
                };
                await this._carBusinessRules.UpdateCarState(command);
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
