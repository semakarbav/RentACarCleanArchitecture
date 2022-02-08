using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Colors.Commands.UpdateColor
{
    public class UpdateColorCommandValidator:AbstractValidator<UpdateColorCommand>
    {
        public UpdateColorCommandValidator()
        {
            RuleFor(b => b.Id).NotEmpty().WithMessage("Renk güncellenirken Id boş geçilemez");
            RuleFor(b => b.Id).GreaterThan(0).WithMessage("Güncellencek olan renk Id'si sıfırdan büyük olmalı");
            RuleFor(b => b.Name).NotEmpty().WithMessage("Güncellenecek renk adı boş geçilemez");
            RuleFor(b => b.Name).MinimumLength(2).WithMessage("Renk ismi minumum 2 karakter olmalı");
        }
    }
}
