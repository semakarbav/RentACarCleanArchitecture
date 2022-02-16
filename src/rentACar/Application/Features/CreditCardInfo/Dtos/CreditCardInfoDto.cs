using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CreditCardInfo.Dtos
{
    public class CreditCardInfoDto
    {
        public string CardHolder { get; set; }
        public string CardNo { get; set; }
        public string Date { get; set; }
        public string Cvv { get; set; }
    }
}
