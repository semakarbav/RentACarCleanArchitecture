using Application.Features.CarDamages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CarDamages.Queries.GetCarDamageList
{
    public class GetCarDamageListQuery : IRequest<CarDamageListModel>
    {
        public PageRequest PageRequest { get; set; }


        public class GetCarDamageListQueryHandler : IRequestHandler<GetCarDamageListQuery, CarDamageListModel>
        {
            ICarDamageRepository _carDamageRepository;
            IMapper _mapper;

            public GetCarDamageListQueryHandler(ICarDamageRepository carDamageRepository, IMapper mapper)
            {
                _carDamageRepository = carDamageRepository;
                _mapper = mapper;
            }

            public async Task<CarDamageListModel> Handle(GetCarDamageListQuery request, CancellationToken cancellationToken)
            {
                var carDamages = await _carDamageRepository.GetListAsync
                    (
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedCarDamageListModel = _mapper.Map<CarDamageListModel>(carDamages);
                return mappedCarDamageListModel;
            }
        }
    }
}
