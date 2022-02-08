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

namespace Application.Features.IndividualCustomers.Commands.UpdateIndividualCustomer
{
    public class UpdateIndividualCustomerCommand: IRequest<IndividualCustomer>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public class UpdateIndividualCustomerCommandHandler : IRequestHandler<UpdateIndividualCustomerCommand, IndividualCustomer>
        {
            IIndividualCustomerRepository _individualRepository;
            IMapper _mapper;
            IndividualCustomerBusinessRules _individualCustomerBusinessRules;

            public UpdateIndividualCustomerCommandHandler(IIndividualCustomerRepository individualRepository, IMapper mapper, IndividualCustomerBusinessRules individualCustomerBusinessRules)
            {
                _individualRepository = individualRepository;
                _mapper = mapper;
                _individualCustomerBusinessRules = individualCustomerBusinessRules;
            }

            public async Task<IndividualCustomer> Handle(UpdateIndividualCustomerCommand request, CancellationToken cancellationToken)
            {
                var individualCustpmerToUpdate = await _individualRepository.GetAsync(c => c.Id == request.Id);
                if (individualCustpmerToUpdate == null)
                {
                    throw new BusinessException("Bireysel Müşteri bulunamadı");
                }
                await _individualCustomerBusinessRules.NationalityIdCanNotBeDuplicated(request.NationalId);
                var mappedIndividualCustoemers = _mapper.Map(request, individualCustpmerToUpdate);
                var updatedIndividualCustomers = await _individualRepository.UpdateAsync(mappedIndividualCustoemers);
                return updatedIndividualCustomers;
            }
        }
    }
}
