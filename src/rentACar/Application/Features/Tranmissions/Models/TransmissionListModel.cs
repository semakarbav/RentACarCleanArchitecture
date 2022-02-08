using Application.Features.Tranmissions.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tranmissions.Models
{
    public class TransmissionListModel:BasePageableModel
    {
        public IList<TransmissionListDto> Items { get; set; }
    }
}
