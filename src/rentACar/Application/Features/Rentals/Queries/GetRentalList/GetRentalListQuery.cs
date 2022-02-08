using Application.Features.Rentals.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Queries.GetRentalList
{
    public class GetRentalListQuery:IRequest<RentalListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetRentalListQueryHandler:IRequestHandler<GetRentalListQuery, RentalListModel>
        {
            IRentalRepository _rentalRepository;
            IMapper _mapper;

            public GetRentalListQueryHandler(IRentalRepository rentalRepository, IMapper mapper)
            {
                _rentalRepository = rentalRepository;
                _mapper = mapper;
            }

            public async Task<RentalListModel> Handle(GetRentalListQuery request, CancellationToken cancellationToken)
            {
                var rentals = await _rentalRepository.GetListAsync(
                    index:request.PageRequest.Page,
                    size: request.PageRequest.PageSize);

                var mappedRental = _mapper.Map<RentalListModel>(rentals);
                return mappedRental;
            }
        }
    }
}
