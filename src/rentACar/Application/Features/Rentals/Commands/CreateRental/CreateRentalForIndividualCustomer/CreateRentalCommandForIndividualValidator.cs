using Application.Features.Rentals.Commands.CreateRental.CreateRentalForCorporateCustomer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.CreateRental.CreateRentalForIndividualCustomer
{
    public class CreateRentalCommandValidator : AbstractValidator<RentForIndividualCustomerCommand>
    {
        public CreateRentalCommandValidator()
        {
            RuleFor(c => c.RentDate).LessThan(c => c.ReturnDate).WithMessage("Dönüş tarihi kiralandığı tarihten bugünden küçük olamaz");
            // RuleFor(c => c.ReturnDate).GreaterThan(c => c.RentDate).WithMessage("kiradan dönüş tarihi kiralandığı tarihten büyük olmalı");
        }
    }
}
