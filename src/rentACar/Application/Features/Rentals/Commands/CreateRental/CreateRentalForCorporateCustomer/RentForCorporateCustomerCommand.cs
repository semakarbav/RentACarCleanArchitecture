using Application.Features.Cars.Commands.UpdateCarState;
using Application.Features.Cars.Rules;
using Application.Features.CorporateCustomers.Rules;
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
    public class RentForCorporateCustomerCommand : IRequest<Rental>
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentCityId { get; set; }
        public int ReturnCityId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        public class RentForIndividualCustomerCommandHandler : IRequestHandler<RentForCorporateCustomerCommand, Rental>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
            CarBusinessRules _carBusinessRules;
            InvoiceBusinessRules _invoiceBusinessRules;
            ModelBusinessRules _modelBusinessService;

            public RentForIndividualCustomerCommandHandler(IRentalRepository rentalRepository, IMapper mapper, RentalBusinessRules rentalBusinessRules, CorporateCustomerBusinessRules corporateCustomerBusinessRules, CarBusinessRules carBusinessRules, InvoiceBusinessRules invoiceBusinessRules, ModelBusinessRules modelBusinessService)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
                _carBusinessRules = carBusinessRules;
                _invoiceBusinessRules = invoiceBusinessRules;
                _modelBusinessService = modelBusinessService;
            }

            public async Task<Rental> Handle(RentForCorporateCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var car = await this._carBusinessRules.CheckIfCarIdShouldBeExist(request.CarId);
                _rentalBusinessRules.CheckIfCarIsUnderMaintenance(request.CarId);
                _rentalBusinessRules.CheckIfCarIsRented(request.CarId);
                await _rentalBusinessRules.CheckIfIndividualFindexScoreIsEnough(request.CarId, request.CustomerId);
                await _corporateCustomerBusinessRules.GetTaxNumber(request.CustomerId);



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
                    InvoiceNo = 120,
                    RentalId = createdRental.Id,
                    TotalSum = 1000
                };
                await this._invoiceBusinessRules.MakeInvoice(invoiceCommand);


                return createdRental;
            }


        }
    
    }
}
