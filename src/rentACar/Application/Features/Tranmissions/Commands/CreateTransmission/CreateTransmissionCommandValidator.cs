using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tranmissions.Commands.CreateTransmission
{
    public class CreateTransmissionCommandValidator: AbstractValidator<CreateTransmissionCommand>
    {
        public CreateTransmissionCommandValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("Vites tipi Boş geçilemez");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Vites tipi minumum 2 karakter olmalı");
        }
    }
}
