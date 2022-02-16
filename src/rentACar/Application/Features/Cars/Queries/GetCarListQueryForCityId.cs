using Application.Features.Cars.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Queries
{
    public class GetCarListQueryForCityId : IRequest<CarListModel>
    {
        public PageRequest PageRequest { get; set; }
        public int CityId { get; set; }

        public class GetCarListQueryForCityIdHandler : IRequestHandler<GetCarListQueryForCityId, CarListModel>
        {
            ICarRepository _carRepository;
            IMapper _mapper;

            public GetCarListQueryForCityIdHandler(ICarRepository carRepository, IMapper mapper)
            {
                _carRepository = carRepository;
                _mapper = mapper;
            }

            public async Task<CarListModel> Handle(GetCarListQueryForCityId request,
                CancellationToken cancellationToken)
            {
                var cars = await _carRepository.GetListAsync(
                    c => c.CityId == request.CityId,
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                var mappedCars = _mapper.Map<CarListModel>(cars);
                return mappedCars;
            }
        }
    }
}
