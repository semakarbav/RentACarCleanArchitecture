using Application.Features.CarDamages.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CarDamages.Models
{
    public class CarDamageListModel :  BasePageableModel
    {
        public IList<CarDamageListDto> Items { get; set; }

    }
}
