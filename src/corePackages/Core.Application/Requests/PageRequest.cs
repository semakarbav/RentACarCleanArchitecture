using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Requests
{
    public class PageRequest
    {
        public int Page { get; set; } //kaçıncı sayfa
        public int PageSize { get; set; } //bir sayfada kaç data olsun
    }
}
