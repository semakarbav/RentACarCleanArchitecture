using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.IndividualCustomers.Rules
{
    public class IndividualCustomerBusinessRules
    {
        IIndividualCustomerRepository _individualRepository;

        public IndividualCustomerBusinessRules(IIndividualCustomerRepository individualRepository)
        {
            _individualRepository = individualRepository;
        }
        public async Task NationalityIdCanNotBeDuplicated(string nationalityId)
        {
            var result = await _individualRepository.GetListAsync(b => b.NationalId == nationalityId);
            if (result.Items.Any())
            {
                throw new BusinessException("National Id exists");
            }
        }
        public async Task<string> GetNationalId(int id)
        {
            var customer = await this._individualRepository.GetAsync(c => c.Id == id);
            if (customer == null)
            {
                throw new BusinessException("Bireysel müşteri bulunamadı");
            }
            return customer.NationalId;
        }
    }
}
