using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Rules
{
    public class ModelBusinessRules
    {
        IModelRepository _modelRepository;

        public ModelBusinessRules(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }
        public async Task ModelNameCanNotBeDuplicatedWhenInserted(string name)
        {
            var result = await _modelRepository.GetListAsync(m => m.Name == name);
            if (result.Items.Any())
            {
                throw new BusinessException("Model zaten var");
            }
        }
        public async Task CheckIfBrandIsEmpty(int brandId)
        {
            var result = await _modelRepository.GetAsync(m => m.Id == brandId);
            if (result==null)
            {
                throw new BusinessException("Marka bulunamamdı");
            }
        }
        public async Task<double> GetDailyPriceById(int modelId)
        {
            var result = await _modelRepository.GetAsync(m => m.Id == modelId);

            if (result == null)
            {
                throw new BusinessException("Model bulunamadı");
            }
            return result.DailyPrice;
        }
    }
}
