using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Colors.Commands.DeleteColor
{
    public class DeleteColorCommandValidator:AbstractValidator<DeleteColorCommand>
    {
        public DeleteColorCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Silinecek rengin Id'si boş olamaz");
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Silinecek olan rengin Id'si sıfırdan büyük olmalı");
        }
    }
}
