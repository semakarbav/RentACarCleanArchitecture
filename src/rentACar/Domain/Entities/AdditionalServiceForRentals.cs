using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AdditionalServiceForRentals :Entity
    {
        public int RentalId { get; set; }
        public int AdditionalServiceId { get; set; }
        public virtual Rental Rental { get; set; }
        public virtual AdditionalService AdditionalService { get; set; }

        public AdditionalServiceForRentals()
        {

        }
        public AdditionalServiceForRentals(int id, int rentalId, int additionalServiceId)
        {
            Id = id;
            RentalId = rentalId;
            AdditionalServiceId = additionalServiceId;

        }
    }
}
