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

namespace Application.Features.CorporateCustomers.Commands.DeleteCorporateCustomer
{
    public class DeleteCorporateCustomerCommand:IRequest<CorporateCustomer>
    {
        public int Id { get; set; }
        public class DeleteCorporateCustomerCommandHandler : IRequestHandler<DeleteCorporateCustomerCommand, CorporateCustomer>
        {
            ICorporateCustomerRepository _corporateCustomerRepository;
            IMapper _mapper;

            public DeleteCorporateCustomerCommandHandler(ICorporateCustomerRepository corporateCustomerRepository, IMapper mapper)
            {
                _corporateCustomerRepository = corporateCustomerRepository;
                _mapper = mapper;
            }

            public async Task<CorporateCustomer> Handle(DeleteCorporateCustomerCommand request, CancellationToken cancellationToken)
            {
                var corporateCustomerToDelete = _corporateCustomerRepository.GetAsync(c => c.Id == request.Id);
                if (corporateCustomerToDelete == null)
                {
                    throw new BusinessException("Kurumsal Müşteri bulunamadı");
                }

                //var mappedIndividualCustoemers = _mapper.Map<IndividualCustomer>(request);
                var deletedCorporateCustomers = await _corporateCustomerRepository.DeleteAsync(await corporateCustomerToDelete);

                return deletedCorporateCustomers;
            }
        }
    }
}
