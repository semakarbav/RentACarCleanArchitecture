using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tranmissions.Rules
{
    public class TransmissionBusinessRules
    {
        ITransmissionRepository _transmissonRepository;

        public TransmissionBusinessRules(ITransmissionRepository transmissonRepository)
        {
            _transmissonRepository = transmissonRepository;
        }
        public async Task TransmissionNameCanNotBeDuplicatedWhenInserted(string name)
        {
            var result = await _transmissonRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any())
            {
                throw new BusinessException("Transmission name exists");
            }
        }
    }
}
