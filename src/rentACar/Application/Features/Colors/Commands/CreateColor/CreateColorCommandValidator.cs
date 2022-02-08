using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Colors.Commands.CreateColor
{
    public class CreateColorCommandValidator : AbstractValidator<CreateColorCommand>
    {
        public CreateColorCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Renk Boş geçilemez");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Renk minumum 2 karakter olmalı");
        }
    }
}
