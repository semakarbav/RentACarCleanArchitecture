using Application.Features.Cars.Commands.UpdateCarState;
using Application.Features.Cars.Rules;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Invoices.Command.CreateInvoice;
using Application.Features.Invoices.Rules;
using Application.Features.Models.Rules;
using Application.Features.Rentals.Dtos;
using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Adapter;
using Core.Application.Pipelines.Logging;
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
    public class RentForIndividualCustomerCommand : IRequest<CreatedRentalDto> ,ILoggableRequest
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentCityId { get; set; }
        public int ReturnCityId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int RentedKilometer { get; set; }
        

        public class RentForIndividualCustomerCommandHandler : IRequestHandler<RentForIndividualCustomerCommand, CreatedRentalDto>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;
            RentalBusinessRules _rentalBusinessRules;
            IndividualCustomerBusinessRules _individualCustomerBusinessRules;
            CarBusinessRules _carBusinessRules;
            InvoiceBusinessRules _invoiceBusinessRules;
            ModelBusinessRules _modelBusinessService;
            IFindexScoreAdapterService _findexScoreAdapterService;

            public RentForIndividualCustomerCommandHandler(IRentalRepository rentalRepository, IMapper mapper,
                RentalBusinessRules rentalBusinessRules, IndividualCustomerBusinessRules individualCustomerBusinessRules, 
                CarBusinessRules carBusinessRules, InvoiceBusinessRules invoiceBusinessRules, ModelBusinessRules modelBusinessService, IFindexScoreAdapterService findexScoreAdapterService)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
                _rentalBusinessRules = rentalBusinessRules;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
                _carBusinessRules = carBusinessRules;
                _invoiceBusinessRules = invoiceBusinessRules;
                _modelBusinessService = modelBusinessService;
                _findexScoreAdapterService = findexScoreAdapterService;
            }

            public async Task<CreatedRentalDto> Handle(RentForIndividualCustomerCommand request,
                CancellationToken cancellationToken)
            {
                var car = await this._carBusinessRules.CheckIfCarIsExist(request.CarId);
                await _carBusinessRules.CheckIfCarIsMaintenance(request.CarId);
                await _carBusinessRules.CheckIfCarIsRented(request.CarId);
                await _rentalBusinessRules.CheckIfIndividualFindexScoreIsEnough(request.CarId, request.CustomerId);
               

                var mappedRental = _mapper.Map<Rental>(request);
                mappedRental.RentedKilometer = car.Kilometer;

                var createdRental = await _rentalRepository.AddAsync(mappedRental);

                UpdateCarStateCommand command = new UpdateCarStateCommand
                {
                    Id = request.CarId,
                    CarState = CarState.Rented
                };
                await this._carBusinessRules.UpdateCarState(command);

                Random random = new Random();
                CreateInvoiceCommand invoiceCommand = new CreateInvoiceCommand()
                {
                    CustomerId = request.CustomerId,
                    InvoiceDate = DateTime.Now,
                    InvoiceNo = random.Next(0, 100000),
                    RentalId = createdRental.Id,
                    TotalSum = 1000
                };
                await _invoiceBusinessRules.CreateInvoice(invoiceCommand);



                var createdRentalDto = _mapper.Map<CreatedRentalDto>(createdRental);
                return createdRentalDto;
               
            }

           
        }
    }
}
