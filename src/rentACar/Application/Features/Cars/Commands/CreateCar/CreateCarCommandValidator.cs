using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.CreateCar
{
    public class CreateCarCommandValidator:AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(c => c.ModelYear).NotEmpty().WithMessage("Eklenecek arabanın model yılı boş geçilemez");
            RuleFor(c => c.Plate).NotEmpty().WithMessage("Eklenecek arabanın plakası boş geçilemez");
            RuleFor(c => c.ModelId).NotEmpty().WithMessage("Eklenecek arabanın modeli boş geçilemez");
            RuleFor(c => c.ColorId).NotEmpty().WithMessage("Eklenecek arabanın rengi boş geçilemez"); ;
            RuleFor(c => c.ColorId).GreaterThan(0).WithMessage("Eklenecek arabanın color Id sıfırdan büyük olmalı");
            RuleFor(c => c.ModelId).GreaterThan(0).WithMessage("Eklenecek arabanın model Id sıfırdan büyük olmalı");
            RuleFor(c => c.Plate).Length(6, 9).WithMessage("Plaka karakter uzunluğu 6-9 arası giriniz");
            RuleFor(c => c.Plate).Must(StartWithNumber).WithMessage("Uygun formatta plaka giriniz");
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
