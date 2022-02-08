using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tranmissions.Commands.UpdateTransmission
{
    public class UpdateTransmissionCommandValidator: AbstractValidator<UpdateTransmissionCommand>
    {
        public UpdateTransmissionCommandValidator()
        {
            RuleFor(b => b.Id).NotEmpty().WithMessage("Vites tipi güncellenirken Id boş geçilemez");
            RuleFor(b => b.Id).GreaterThan(0).WithMessage("Güncellencek olan vites tipi Id'si sıfırdan büyük olmalı");
            RuleFor(b => b.Name).NotEmpty().WithMessage("Güncellenecek vites tipi adı boş geçilemez");
            RuleFor(b => b.Name).MinimumLength(2).WithMessage("Vites tipi ismi minumum 2 karakter olmalı");
        }
    }
}
