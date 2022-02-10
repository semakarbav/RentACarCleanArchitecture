using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CorporateCustomer:Customer
    {
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        
        public CorporateCustomer(int id, string email, string companyName, string taxNumber,int customerId):this()
        {
            Id = id;
            CustomerId = customerId;
            Email = email;
            CompanyName = companyName;
            TaxNumber = taxNumber;
        }
        public CorporateCustomer()
        {

        }
    }
}
