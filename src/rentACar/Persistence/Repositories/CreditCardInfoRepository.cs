using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CreditCardInfoRepository : EfRepositoryBase<CreditCardInfo, BaseDbContext>, ICreditCardInfoRepository
    {
        public CreditCardInfoRepository(BaseDbContext context) : base(context)
        {
        }
    
    }
}
