using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.CreateRental.CreateRentalForCorporateCustomer
{
    public class CreateRentalCommandValidator : AbstractValidator<RentForCorporateCustomerCommand>
    {
        public CreateRentalCommandValidator()
        {
            
            RuleFor(c => c.ReturnDate).GreaterThan(c => c.RentDate).WithMessage("kiradan dönüş tarihi kiralandığı tarihten büyük olmalı");
        }
    }
}
