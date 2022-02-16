using Application.Features.Cars.Rules;
using Application.Services.Repositories;
using Core.Application.Adapter;
using Core.Application.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FindexScore
{
    public class FindexScoreAdapterManager : IFindexScoreAdapterService
    {
        FindexScoreService findexScoreService = new FindexScoreService();

        public Task<int> GetScoreOfCorporateCustomer(string taxNumber)
        {
            Task<int> result = Task.Run(() =>
            {

                return findexScoreService.GetScoreOfCorporateCustomer(taxNumber);
            });
            return result;
        }

        public Task<int> GetScoreOfIndividualCustomer(string nationalId)
        {
            Task<int> result = Task.Run(() =>
            {

                return findexScoreService.GetScoreOfIndividualCustomer(nationalId);
            });
            return result;
        }
    }
}
