using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CreditCardInfo: Entity
    {
        public CreditCardInfo(int id,string cardHolder, string cardNo, string date, string cvv, int customerId, Customer customer)
        {
            Id = id;
            CardHolder = cardHolder;
            CardNo = cardNo;
            Date = date;
            Cvv = cvv;
            CustomerId = customerId;
            Customer = customer;
        }
        public CreditCardInfo()
        {

        }

        public string CardHolder { get; set; }
        public string CardNo { get; set; }
        public string Date { get; set; }
        public string Cvv { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
