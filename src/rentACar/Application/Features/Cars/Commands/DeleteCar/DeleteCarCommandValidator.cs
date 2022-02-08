using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.DeleteCar
{
    public class DeleteCarCommandValidator : AbstractValidator<DeleteCarCommand>
    {
        public DeleteCarCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Silinecek arabanın Id'si boş geçilemez");
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Silinecek arabanın  Id'si sıfırdan büyük olmalı");
        }
    }
}
