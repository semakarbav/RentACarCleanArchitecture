using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ExternalServices
{
    public class FindexScoreService
    {
		public int GetScoreOfIndividualCustomer(String nationalId)
		{
			Random random = new Random();
			return random.Next(0,1901);
		}
		public int GetScoreOfCorporateCustomer(String taxNumber)
		{
			Random random = new Random();
			return random.Next(0,1901);
		}
	}
}
