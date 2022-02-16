using Application.Features.CreditCardInfo.Dtos;
using Core.Application.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PosService
{
    public class FakePosServiceAdapterManager : IPosService
    {
        FakePosService fakePosService = new FakePosService();
        public Task<bool> MakePayment(CreditCardInfoDto creditCardInfoDto)
        {
            var result =fakePosService.GetPayment(creditCardInfoDto.CardHolder,
                creditCardInfoDto.CardNo, creditCardInfoDto.Date, creditCardInfoDto.Cvv);
            return Task.FromResult(result);
        }
    }
}
