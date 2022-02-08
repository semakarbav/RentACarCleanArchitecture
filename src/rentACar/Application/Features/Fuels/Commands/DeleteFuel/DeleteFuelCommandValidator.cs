using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Commands.DeleteFuel
{
    public class DeleteFuelCommandValidator:AbstractValidator<DeleteFuelCommand>
    {
        public DeleteFuelCommandValidator()
        {
            RuleFor(f => f.Id).NotEmpty().WithMessage("Silinecek yakıt tipinin Id'si boş olamaz");
            RuleFor(f => f.Id).GreaterThan(0).WithMessage("Silinecek olan yakıt tipinin Id'si sıfırdan büyük olmalı");
        }
    }
}
