using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CorporateCustomers.Rules
{
    public class CorporateCustomerBusinessRules
    {
        ICorporateCustomerRepository _corporateCustomerRepository;

        public CorporateCustomerBusinessRules(ICorporateCustomerRepository corporateCustomerRepository)
        {
            _corporateCustomerRepository = corporateCustomerRepository;
        }
        public async Task TaxNumberCanNotBeDuplicated(string taxNumber)
        {
            var result = await _corporateCustomerRepository.GetListAsync(b => b.TaxNumber == taxNumber);
            if (result.Items.Any())
            {
                throw new BusinessException("Taz Number Id exists");
            }
        }
        public async Task<string> GetTaxNumber(int id)
        {
            var customer = await this._corporateCustomerRepository.GetAsync(c => c.Id == id);
            if (customer ==null)
            {
                throw new BusinessException("Kurumsal müşteri bulunamadı");
            }
            return customer.TaxNumber;
        }
    }
}
