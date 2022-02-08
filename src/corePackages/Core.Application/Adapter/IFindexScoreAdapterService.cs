using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Adapter
{
    public interface IFindexScoreAdapterService
    {
        Task<int> GetScoreOfIndividualCustomer(String nationalId);
        Task<int> GetScoreOfCorporateCustomer(String taxNumber);
    }
}
