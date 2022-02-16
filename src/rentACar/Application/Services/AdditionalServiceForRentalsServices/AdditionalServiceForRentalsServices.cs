using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Services.AdditionalServiceForRentalsServices
{
    public class AdditionalServiceForRentalsServices : IAdditionalServiceForRentalsService
    {
        IAdditionalServiceForRentalsRepository _additionalServiceForRentalsRepository;

        public AdditionalServiceForRentalsServices(IAdditionalServiceForRentalsRepository additionalServiceForRentalsRepository)
        {
            _additionalServiceForRentalsRepository = additionalServiceForRentalsRepository;
        }

        public async Task<IList<AdditionalServiceForRentals>> AddByRentalIdAndAdditionalServices(int rentalId, IList<AdditionalService> additionalServices)
        {
            var additionalServicesForRentals = additionalServices.Select(a => new AdditionalServiceForRentals
           {
                RentalId = rentalId,
                AdditionalServiceId = a.Id
            }).ToList();

            foreach (var item in additionalServicesForRentals)
                await _additionalServiceForRentalsRepository.AddAsync(item);

            return additionalServicesForRentals;
        }
    }
}
