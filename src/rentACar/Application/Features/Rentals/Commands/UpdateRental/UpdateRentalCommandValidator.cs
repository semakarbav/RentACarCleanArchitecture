using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Rentals.Commands.UpdateRental
{
    public class UpdateRentalCommandValidator:AbstractValidator<UpdateRentalCommand>
    {
        public UpdateRentalCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.RentDate).GreaterThan(DateTime.Now).LessThan(c => c.ReturnDate).WithMessage("Dönüş tarihi kiralandığı tarihten küçük olamaz");
            RuleFor(c => c.ReturnDate).GreaterThan(c => c.RentDate).WithMessage("kiradan dönüş tarihi kiralandığı tarihten büyük olmalı");
        }
    }
}
