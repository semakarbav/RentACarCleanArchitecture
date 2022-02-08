using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Commands.CreateFuel
{
    public class CreateFuelCommandValidator:AbstractValidator<CreateFuelCommand>
    {
        public CreateFuelCommandValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("Yakıt tipi Boş geçilemez");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Yakıt tipi minumum 2 karakter olmalı");
        }
    }
}
