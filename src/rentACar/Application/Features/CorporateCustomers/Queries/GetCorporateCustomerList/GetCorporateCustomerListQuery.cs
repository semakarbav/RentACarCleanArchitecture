using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CorporateCustomers.Queries.GetCorporateCustomerList
{
    public  class GetCorporateCustomerListQuery : IRequest<CorporateCustomerListModel>
    {
        public PageRequest PageRequest { get; set; }
    }
    public class GetCorporateCustomerListQueryHandler : IRequestHandler<GetCorporateCustomerListQuery, CorporateCustomerListModel>
    {
        ICorporateCustomerRepository _corporateRepository;
        IMapper _mapper;

        public GetCorporateCustomerListQueryHandler(ICorporateCustomerRepository corporateRepository, IMapper mapper)
        {
            _corporateRepository = corporateRepository;
            _mapper = mapper;
        }

        public async Task<CorporateCustomerListModel> Handle(GetCorporateCustomerListQuery request, CancellationToken cancellationToken)
        {
            var corporateCustomer = await _corporateRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
                );
            var mappedCorporateCustomers = _mapper.Map<CorporateCustomerListModel>(corporateCustomer);
            return mappedCorporateCustomers;
        }
    }

}
