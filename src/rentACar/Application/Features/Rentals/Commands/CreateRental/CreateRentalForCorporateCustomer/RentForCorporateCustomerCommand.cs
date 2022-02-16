using Application.Features.AdditionalServices.Rules;
using Application.Features.Cars.Commands.UpdateCarState;
using Application.Features.Cars.Rules;
using Application.Features.CorporateCustomers.Rules;
using Application.Features.CreditCardInfo.Dtos;
using Application.Features.Invoices.Command.CreateInvoice;
using Application.Features.Invoices.Rules;
using Application.Features.Models.Rules;
using Application.Features.Payments.Commands.CreatePayment;
using Application.Features.Payments.Rules;
using Application.Features.Rentals.Rules;
using Application.Services.AdditionalServiceForRentalsServices;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
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
    public class RentForCorporateCustomerCommand : IRequest<Rental>, ITransactionRequest
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentCityId { get; set; }
        public int ReturnCityId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int RentedKilometer { get; set; }
        public List<int>? AdditionalServiceIds { get; set; }
        public CreditCardInfoDto CreditCardInfoDto { get; set; }


        public class RentForIndividualCustomerCommandHandler : IRequestHandler<RentForCorporateCustomerCommand, Rental>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            CorporateCustomerBusinessRules _corporateCustomerBusinessRules;
            CarBusinessRules _carBusinessRules;
            InvoiceBusinessRules _invoiceBusinessRules;
            ModelBusinessRules _modelBusinessRules;
            PaymentBusinessRules _paymentBusinessRules;
            AdditionalServiceBusinessRules _additionalServiceBusinessRules;
            IAdditionalServiceForRentalsService _additionalServiceForRentalsService;

            public RentForIndividualCustomerCommandHandler(IRentalRepository rentalRepository, IMapper mapper, RentalBusinessRules rentalBusinessRules, CorporateCustomerBusinessRules corporateCustomerBusinessRules, CarBusinessRules carBusinessRules, InvoiceBusinessRules invoiceBusinessRules, ModelBusinessRules modelBusinessRules, PaymentBusinessRules paymentBusinessRules, AdditionalServiceBusinessRules additionalServiceBusinessRules, IAdditionalServiceForRentalsService additionalServiceForRentalsService)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
                _carBusinessRules = carBusinessRules;
                _invoiceBusinessRules = invoiceBusinessRules;
                _modelBusinessRules = modelBusinessRules;
                _paymentBusinessRules = paymentBusinessRules;
                _additionalServiceBusinessRules = additionalServiceBusinessRules;
                _additionalServiceForRentalsService = additionalServiceForRentalsService;
            }

            public async Task<Rental> Handle(RentForCorporateCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var car = await this._carBusinessRules.CheckIfCarIsExist(request.CarId);
                await _carBusinessRules.CheckIfCarIsMaintenance(request.CarId);
                await _carBusinessRules.CheckIfCarIsRented(request.CarId);
                await _rentalBusinessRules.CheckIfCorporateFindexScoreIsEnough(request.CarId, request.CustomerId);
                await _corporateCustomerBusinessRules.GetTaxNumber(request.CustomerId);



                var mappedRental = _mapper.Map<Rental>(request);
                mappedRental.RentedKilometer = car.Kilometer;
                var additionalServices = await _additionalServiceBusinessRules.GetListByIds(request.AdditionalServiceIds);

                double totalAdditionalServicesPrice = additionalServices.Sum(a => a.Price);
                var totalDays = (request.ReturnDate.Date - request.RentDate.Date).Days + 1;

                var dailyPrice = await _modelBusinessRules.GetDailyPriceById(car.ModelId);
                var total = dailyPrice * totalDays;

                bool differentCities = request.RentCityId != request.ReturnCityId;
                if (differentCities)
                {
                    total += 500;
                }

                var totalPrice = total + totalAdditionalServicesPrice;

                var createdRental = await _rentalRepository.AddAsync(mappedRental);
                await _additionalServiceForRentalsService.AddByRentalIdAndAdditionalServices(createdRental.Id, additionalServices);


                UpdateCarStateCommand command = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Rented
                };
                await this._carBusinessRules.UpdateCarState(command);

                CreatePaymentCommand paymentCommand = new CreatePaymentCommand
                {
                    CreditCardInfoDto = request.CreditCardInfoDto,
                    PaymentDate = DateTime.Now,
                    RentalId = createdRental.Id,
                    TotalSum = totalPrice
                };
                await this._paymentBusinessRules.MakePayment(paymentCommand);

                Random random = new Random();
                CreateInvoiceCommand invoiceCommand = new CreateInvoiceCommand()
                {

                    CustomerId = request.CustomerId,
                    InvoiceDate = DateTime.Now,
                    InvoiceNo = random.Next(0,100000),
                    RentalId = createdRental.Id,
                    TotalSum = totalPrice
                };
                await _invoiceBusinessRules.CreateInvoice(invoiceCommand);


                return createdRental;
            }


        }
    
    }
}
