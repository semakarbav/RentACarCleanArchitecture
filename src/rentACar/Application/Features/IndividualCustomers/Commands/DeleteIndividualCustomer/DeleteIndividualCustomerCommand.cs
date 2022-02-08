using Application.Features.IndividualCustomers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.IndividualCustomers.Commands.DeleteIndividualCustomer
{
    public class DeleteIndividualCustomerCommand: IRequest<IndividualCustomer>
    {
        public int Id { get; set; }
        public class DeleteIndividualCustomerCommandHandler : IRequestHandler<DeleteIndividualCustomerCommand, IndividualCustomer>
        {
            IIndividualCustomerRepository _individualRepository;
            IMapper _mapper;
            IndividualCustomerBusinessRules _individualCustomerBusinessRules;

            public DeleteIndividualCustomerCommandHandler(IIndividualCustomerRepository individualRepository, IMapper mapper, IndividualCustomerBusinessRules individualCustomerBusinessRules)
            {
                _individualRepository = individualRepository;
                _mapper = mapper;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
            }

            public async Task<IndividualCustomer> Handle(DeleteIndividualCustomerCommand request, CancellationToken cancellationToken)
            {
                var individualCustomerToDelete= _individualRepository.GetAsync(c => c.Id == request.Id);
                if (individualCustomerToDelete == null)
                {
                    throw new BusinessException("Bireysel Müşteri bulunamadı");
                }

                //var mappedIndividualCustoemers = _mapper.Map<IndividualCustomer>(request);
                var deletedIndividualCustoemers = await _individualRepository.DeleteAsync(await individualCustomerToDelete);

                return deletedIndividualCustoemers;
            }
        }
    }
}
