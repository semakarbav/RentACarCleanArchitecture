using Application.Features.Cars.Rules;
using Application.Features.CorporateCustomers.Rules;
using Application.Features.IndividualCustomers.Rules;
using Application.Features.Maintenances.Rules;
using Application.Services.Repositories;
using Core.Application.Adapter;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Rules
{
    public class RentalBusinessRules
    {
         IRentalRepository _rentalRepository;
         MaintenanceBusinessRules _maintenanceBusinessRules;
        IndividualCustomerBusinessRules _individualCustomerBusinessRules;
        CarBusinessRules _carBusinessRules;
        CorporateCustomerBusinessRules _corporateBusinessRules;
        IFindexScoreAdapterService _findexScoreAdapterService;

        public RentalBusinessRules(IRentalRepository rentalRepository, MaintenanceBusinessRules maintenanceBusinessRules, IndividualCustomerBusinessRules individualCustomerBusinessRules, CarBusinessRules carBusinessRules, CorporateCustomerBusinessRules corporateBusinessRules, IFindexScoreAdapterService findexScoreAdapterService)
        {
            _rentalRepository = rentalRepository;
            _maintenanceBusinessRules = maintenanceBusinessRules;
            _individualCustomerBusinessRules = individualCustomerBusinessRules;
            _carBusinessRules = carBusinessRules;
            _corporateBusinessRules = corporateBusinessRules;
            _findexScoreAdapterService = findexScoreAdapterService;
        }

        public bool CheckIfCarIsUnderMaintenance(int carId)
        {
            var result = _maintenanceBusinessRules.CheckIfCarIsUnderMaintenance(carId);

            return result;
        }

        public bool CheckIfCarIsRented(int carId)
        {
            var result = _rentalRepository.CheckIfCarIsRented(carId);

            if (result)
            {
                throw new BusinessException("Araba kirada.");
            }
            return result;
        }
        public async Task<bool> CheckIfIndividualFindexScoreIsEnough(int carId, int customerId)
        {
            var nationalId = await this._individualCustomerBusinessRules.GetNationalId(customerId);
            var carFindexScore = await this._carBusinessRules.GetFindexScoreById(carId);
            var findexScore = await this._findexScoreAdapterService.GetScoreOfIndividualCustomer(nationalId);

            if (findexScore < carFindexScore)
            {
                throw new BusinessException("Findex Score yeterli değil");
            }
            return true;
        }

        public async Task<bool> CheckIfCorporateFindexScoreIsEnough(int carId, int customerId)
        {
            var taxNumber = await this._corporateBusinessRules.GetTaxNumber(customerId);
            var carFindexScore = await this._carBusinessRules.GetFindexScoreById(carId);
            var findexScore = await this._findexScoreAdapterService.GetScoreOfCorporateCustomer(taxNumber);

            if (findexScore < carFindexScore)
            {
                throw new BusinessException("Findex score yeterli değil");
            }
            return true;
        }
        public async Task CheckIfKilometerControls(int carId, int kilometer)
        {
            var carToCheck = await this._carBusinessRules.CheckIfCarIsExist(carId);
            if (carToCheck == null)
            {
                throw new BusinessException("Araba bulunamadı");
            }
            if (carToCheck.Kilometer > kilometer)
            {
                throw new BusinessException("Kilometre hatalı");
            }
        }
    }
}
