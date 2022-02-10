using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class IndividualCustomer:Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
       
        public IndividualCustomer(int id, int customerId, string email,string firstName, string lastName, string nationalId):this()
        {
            Id = id;
            CustomerId = customerId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            NationalId = nationalId;
        }
        public IndividualCustomer()
        {

        }
    }
}
