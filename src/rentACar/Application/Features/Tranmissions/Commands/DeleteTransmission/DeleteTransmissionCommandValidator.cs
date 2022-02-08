using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tranmissions.Commands.DeleteTransmission
{
    public class DeleteTransmissionCommandValidator: AbstractValidator<DeleteTransmissionCommand>
    {
        public DeleteTransmissionCommandValidator()
        {
            RuleFor(f => f.Id).NotEmpty().WithMessage("Silinecek vites tipinin Id'si boş olamaz");
            RuleFor(f => f.Id).GreaterThan(0).WithMessage("Silinecek olan vites tipinin Id'si sıfırdan büyük olmalı");
        }
    }
}
