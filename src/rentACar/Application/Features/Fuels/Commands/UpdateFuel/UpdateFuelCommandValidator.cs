using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Fuels.Commands.UpdateFuel
{
    public class UpdateFuelCommandValidator:AbstractValidator<UpdateFuelCommand>
    {
        public UpdateFuelCommandValidator()
        {
            RuleFor(b => b.Id).NotEmpty().WithMessage("Yakıt tipi güncellenirken Id boş geçilemez");
            RuleFor(b => b.Id).GreaterThan(0).WithMessage("Güncellencek olan yakıt tipi Id'si sıfırdan büyük olmalı");
            RuleFor(b => b.Name).NotEmpty().WithMessage("Güncellenecek yakıt tipi adı boş geçilemez");
            RuleFor(b => b.Name).MinimumLength(2).WithMessage("Yakıt tipi ismi minumum 2 karakter olmalı");
        }
    }
}
