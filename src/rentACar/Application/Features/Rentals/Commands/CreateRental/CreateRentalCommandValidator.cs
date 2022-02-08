using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.CreateRental
{
    public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
    {
        public CreateRentalCommandValidator()
        {
            RuleFor(c => c.RentDate).GreaterThan(DateTime.Now).LessThan(c => c.ReturnDate).WithMessage("Dönüş tarihi kiralandığı tarihten bugünden küçük olamaz");
            RuleFor(c => c.ReturnDate).GreaterThan(c => c.RentDate).WithMessage("kiradan dönüş tarihi kiralandığı tarihten büyük olmalı");
        }
    }
}