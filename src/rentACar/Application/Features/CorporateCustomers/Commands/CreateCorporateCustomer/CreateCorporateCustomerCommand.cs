using Application.Features.CorporateCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CorporateCustomers.Commands.CreateCorporateCustomer
{
    public class CreateCorporateCustomerCommand: IRequest<CorporateCustomer>
    {
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        public class CreateCorporateCustomerCommandHandler : IRequestHandler<CreateCorporateCustomerCommand, CorporateCustomer>
        {
            ICorporateCustomerRepository _corporateRepository;
            IMapper _mapper;
            CorporateCustomerBusinessRules _corporateCustomerBusinessRules;

            public CreateCorporateCustomerCommandHandler(ICorporateCustomerRepository corporateRepository, IMapper mapper, CorporateCustomerBusinessRules corporateCustomerBusinessRules)
            {
                _corporateRepository = corporateRepository;
                _mapper = mapper;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
            }

            public async Task<CorporateCustomer> Handle(CreateCorporateCustomerCommand request, CancellationToken cancellationToken)
            {
                await _corporateCustomerBusinessRules.TaxNumberCanNotBeDuplicated(request.TaxNumber);
                var mappedCorporateCustomer = _mapper.Map<CorporateCustomer>(request);
                var createdCcorporateCustomers = await _corporateRepository.AddAsync(mappedCorporateCustomer);
                return createdCcorporateCustomers;
            }
        }
    }
}
