using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rental : Entity
    {
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int RentCityId { get; set; }
        public int ReturnCityId { get; set; }
        public int? ReturnedCityId { get; set; }
        public int CustomerId { get; set; }
        public int RentedKilometer { get; set; }
        public int ReturnedKilometer { get; set; }

        public int CarId { get; set; }

        public City RentCity { get; set; }
        public City ReturnCity { get; set; }
        public City ReturnedCity { get; set; }
        public Car Car { get; set; }
        public virtual Customer Customer { get; set; }


        public Rental()
        {

        }

        public Rental(int id, DateTime rentDate, DateTime returnDate, int customerId,
            DateTime returnedDate, int rentedKilometer, int returnedKilometer, int carId
            , int rentCityId, int returnCityId , int returnedCityId  ) : this()
        {
            Id = id;
            CustomerId = customerId;
            RentCityId = rentCityId;
            ReturnCityId = returnCityId;
            ReturnedCityId = returnedCityId;
            RentDate = rentDate;
            ReturnDate = returnDate;
            ReturnedDate = returnedDate;
            RentedKilometer = rentedKilometer;
            ReturnedKilometer = returnedKilometer;
            CarId = carId;
        }
    }
}
