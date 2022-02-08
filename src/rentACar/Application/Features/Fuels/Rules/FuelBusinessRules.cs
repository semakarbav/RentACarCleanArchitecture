using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Rules
{
    public class FuelBusinessRules
    {
        IFuelRepository _fuelRepository;

        public FuelBusinessRules(IFuelRepository fuelRepository)
        {
            _fuelRepository = fuelRepository;
        }
        public async Task CheckIfFuelTypeCanNotBeDuplicated(string name)
        {
            var results=await _fuelRepository.GetListAsync(f => f.Name == name);
            if (results.Items.Any())
            {
                throw new BusinessException("Fuel type exists");
            }
        }
    }
}
