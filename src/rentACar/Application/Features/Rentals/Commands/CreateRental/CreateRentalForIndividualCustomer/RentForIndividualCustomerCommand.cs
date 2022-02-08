using Application.Features.Cars.Commands.UpdateCarState;
using Application.Features.Cars.Rules;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Invoices.Command.CreateInvoice;
using Application.Features.Invoices.Rules;
using Application.Features.Models.Rules;
using Application.Features.Rentals.Rules;
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

namespace Application.Features.Rentals.Commands.CreateRental.CreateRentalForCorporateCustomer
{
    public class RentForIndividualCustomerCommand : IRequest<Rental>
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentCityId { get; set; }
        public int ReturnCityId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        public class RentForIndividualCustomerCommandHandler : IRequestHandler<RentForIndividualCustomerCommand, Rental>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            IndividualCustomerBusinessRules _individualCustomerBusinessRules;
            CarBusinessRules _carBusinessRules;
            InvoiceBusinessRules _invoiceBusinessRules;
            ModelBusinessRules _modelBusinessService;

            public RentForIndividualCustomerCommandHandler(IRentalRepository rentalRepository, IMapper mapper, RentalBusinessRules rentalBusinessRules, IndividualCustomerBusinessRules individualCustomerBusinessRules, CarBusinessRules carBusinessRules, InvoiceBusinessRules invoiceBusinessRules, ModelBusinessRules modelBusinessService)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
                _carBusinessRules = carBusinessRules;
                _invoiceBusinessRules = invoiceBusinessRules;
                _modelBusinessService = modelBusinessService;
            }

            public async Task<Rental> Handle(RentForIndividualCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var car = await this._carBusinessRules.CheckIfCarIdShouldBeExist(request.CarId);
                _rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);
                _rentalBusinessRules.CheckIfCarIsRented(request.CarId);
                await _rentalBusinessRules.CheckIfIndividualFindexScoreIsEnough(request.CarId, request.CustomerId);
                await _individualCustomerBusinessRules.GetNationalId(request.CustomerId);

                

                var mappedRental = _mapper.Map<Rental>(request);
                mappedRental.RentedKilometer = car.Kilometer;

                var createdRental = await _rentalRepository.AddAsync(mappedRental);

                UpdateCarStateCommand command = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Rented
                };
                await this._carBusinessRules.UpdateCarState(command);

                CreateInvoiceCommand invoiceCommand = new CreateInvoiceCommand()
                {
                    CustomerId = request.CustomerId,
                    InvoiceDate = DateTime.Now,
                    InvoiceNo = 123,
                    RentalId = createdRental.Id,
                    TotalSum = 500
                };
                await this._invoiceBusinessRules.MakeInvoice(invoiceCommand);


                return createdRental;
            }

           
        }
    }
}
