using Application.Features.CorporateCustomers.Rules;
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

namespace Application.Features.CorporateCustomers.Commands.UpdateCorporateCustomer
{
    public class UpdateCorporateCustomerCommand : IRequest<CorporateCustomer>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        public class UpdateCorporateCustomerCommandHandler : IRequestHandler<UpdateCorporateCustomerCommand, CorporateCustomer>
        {
            ICorporateCustomerRepository _corporateRepository;
            IMapper _mapper;
            CorporateCustomerBusinessRules _corporateCustomerBusinessRules;

            public UpdateCorporateCustomerCommandHandler(ICorporateCustomerRepository corporateRepository, IMapper mapper, CorporateCustomerBusinessRules corporateCustomerBusinessRules)
            {
                _corporateRepository = corporateRepository;
                _mapper = mapper;
                _corporateCustomerBusinessRules = corporateCustomerBusinessRules;
            }

            public async Task<CorporateCustomer> Handle(UpdateCorporateCustomerCommand request, CancellationToken cancellationToken)
            {
                var corporateCustomerToUpdate = await _corporateRepository.GetAsync(c => c.Id == request.Id);
                if (corporateCustomerToUpdate == null)
                {
                    throw new BusinessException("Kurumsal Müşteri bulunamadı");
                }
                await _corporateCustomerBusinessRules.TaxNumberCanNotBeDuplicated(request.TaxNumber);
                var mappedCorporateCustoemers = _mapper.Map(request, corporateCustomerToUpdate);
                var updatedCorporateCustomers = await _corporateRepository.UpdateAsync(mappedCorporateCustoemers);
                return updatedCorporateCustomers;
            }
        }
    }
}
