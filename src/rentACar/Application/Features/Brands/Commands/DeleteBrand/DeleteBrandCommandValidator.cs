using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(b => b.Id).NotEmpty().WithMessage("Silinecek markanın Id alanı boş geçilemez");
            RuleFor(b => b.Id).GreaterThan(0).WithMessage("Silinecek olan markanın Id'si sıfırdan büyük olmalı");
        }
    }
}
