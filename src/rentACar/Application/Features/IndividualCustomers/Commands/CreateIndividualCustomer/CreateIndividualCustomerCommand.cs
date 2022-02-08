using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.IndividualCustomers.Commands.CreateIndividualCustomer
{
    public class CreateIndividualCustomerCommand:IRequest<IndividualCustomer>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public class CreateIndividualCustomerCommandHandler: IRequestHandler<CreateIndividualCustomerCommand, IndividualCustomer>
        {
            IIndividualCustomerRepository _individualRepository;
            IMapper _mapper;
            IndividualCustomerBusinessRules _individualCustomerBusinessRules;

            public CreateIndividualCustomerCommandHandler(IIndividualCustomerRepository individualRepository, IMapper mapper, IndividualCustomerBusinessRules individualCustomerBusinessRules)
            {
                _individualRepository = individualRepository;
                _mapper = mapper;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
            }

            public async Task<IndividualCustomer> Handle(CreateIndividualCustomerCommand request, CancellationToken cancellationToken)
            {
                await _individualCustomerBusinessRules.NationalityIdCanNotBeDuplicated(request.NationalId);
                var mappedIndividualCustoemers = _mapper.Map<IndividualCustomer>(request);
                var createdIndividualCustoemers = await _individualRepository.AddAsync(mappedIndividualCustoemers);
                return createdIndividualCustoemers;
            }
        }
    }
}
