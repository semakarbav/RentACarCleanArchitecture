
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.UpdateCar
{
    public class UpdateCarCommandValidator: AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            RuleFor(c=>c.Id).NotEmpty().WithMessage("Güncellenecek arabanın Id'si boş geçilemez");
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Güncellenecek arabanın  Id'si sıfırdan büyük olmalı");
            RuleFor(c => c.ModelYear).NotEmpty().WithMessage("Güncellenecek arabanın model yılı boş geçilemez");
            RuleFor(c => c.Plate).NotEmpty().WithMessage("Güncellenecek arabanın plakası boş geçilemez");
            RuleFor(c => c.ModelId).NotEmpty().WithMessage("Güncellenecek arabanın modeli boş geçilemez");
            RuleFor(c => c.ColorId).NotEmpty().WithMessage("Güncellenecek arabanın rengi boş geçilemez"); ;
            RuleFor(c => c.ColorId).GreaterThan(0).WithMessage("Güncellenecek arabanın color Id sıfırdan büyük olmalı");
            RuleFor(c => c.ModelId).GreaterThan(0).WithMessage("Güncellenecek arabanın model Id sıfırdan büyük olmalı");
            RuleFor(c => c.Plate).Length(6, 9).WithMessage("Güncellenecek  arabanın plakası karakter uzunluğu 6-9 arası giriniz");
            RuleFor(c => c.Plate).Must(StartWithNumber).WithMessage("Güncellenecek arabanın plakasını formatta giriniz");

        }
        private bool StartWithNumber(string plate)
        {
            var firstTwo = plate.Substring(0, 2);
            int tmp = 0;
            bool result = int.TryParse(firstTwo, out tmp);
            return result;
        }
    }
}
