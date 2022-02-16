using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AdditionalServiceForRentalsServices
{
    public interface IAdditionalServiceForRentalsService
    {
        public Task<IList<AdditionalServiceForRentals>> AddByRentalIdAndAdditionalServices(int rentalId, IList<AdditionalService> additionalServices);
    }
}
