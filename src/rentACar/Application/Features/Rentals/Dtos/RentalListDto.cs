using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Dtos
{
    public class RentalListDto
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentedKilometer { get; set; }
        public int ReturnedKilometer { get; set; }
        public int CarId { get; set; }

    }
}
