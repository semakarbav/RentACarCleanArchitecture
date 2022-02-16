using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdditionalServices.Rules
{
    public class AdditionalServiceBusinessRules
    {
        IAdditionalServiceRepository _additionalServiceRepository;

        public AdditionalServiceBusinessRules(IAdditionalServiceRepository additionalServiceRepository)
        {
            _additionalServiceRepository = additionalServiceRepository;
        }
        public async Task CheckIfAdditionalServiceIsExists(int id)
        {
            var result = await _additionalServiceRepository.GetAsync(b => b.Id == id);
            if (result == null)
            {
                throw new BusinessException("Ek hizmet bulunamadı");
            }
                
        }
        public async Task AdditionalServiceNameCanNotBeDuplicated(string name)
        {
            var result = await _additionalServiceRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any())
            {
                throw new BusinessException("Ek hizmet zaten var");
            }

        }

        public async Task<IList<AdditionalService>> GetListByIds(List<int> additionalServiceIds)
        {
            var additionalServices = (await _additionalServiceRepository.GetListAsync(a => additionalServiceIds.Contains(a.Id))).Items;

            return additionalServices;
        }
    }
}
