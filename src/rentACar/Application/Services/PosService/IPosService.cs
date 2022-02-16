using Application.Features.CreditCardInfo.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PosService
{
    public interface IPosService
    {
        Task<bool> MakePayment(CreditCardInfoDto creditCardInfoDto);
    }
}
