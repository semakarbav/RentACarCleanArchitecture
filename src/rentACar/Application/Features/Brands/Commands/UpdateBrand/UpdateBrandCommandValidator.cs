using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandValidator:AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(b => b.Id).NotEmpty().WithMessage("Marka güncellenirken Id boş geçilemez");
            RuleFor(b => b.Id).GreaterThan(0).WithMessage("Güncellencek olan markanın Id'si sıfırdan büyük olmalı");
            RuleFor(b => b.Name).NotEmpty().WithMessage("Güncellenecek marka adı boş geçilemez");
            RuleFor(b => b.Name).MinimumLength(2).WithMessage("Marka ismi minumum 2 karakter olmalı");
        }
    }
}
