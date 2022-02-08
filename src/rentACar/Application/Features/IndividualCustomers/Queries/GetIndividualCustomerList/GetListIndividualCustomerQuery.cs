using Application.Features.IndividualCustomers.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.IndividualCustomers.Queries
{
    public class GetListIndividualCustomerQuery:IRequest<IndividualCustomerListModel>
    {
        public PageRequest PageRequest { get; set; }
    }
    public class GetListIndividualCustomerQueryHandler : IRequestHandler<GetListIndividualCustomerQuery, IndividualCustomerListModel>
    {
        IIndividualCustomerRepository _individualRepository;
        IMapper _mapper;

        public GetListIndividualCustomerQueryHandler(IIndividualCustomerRepository individualRepository, IMapper mapper)
        {
            _individualRepository = individualRepository;
            _mapper = mapper;
        }

        public async Task<IndividualCustomerListModel> Handle(GetListIndividualCustomerQuery request, CancellationToken cancellationToken)
        {
            var individualCustomer = await _individualRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
                );
            var mappedColors = _mapper.Map<IndividualCustomerListModel>(individualCustomer);
            return mappedColors;
        }
    }
}

