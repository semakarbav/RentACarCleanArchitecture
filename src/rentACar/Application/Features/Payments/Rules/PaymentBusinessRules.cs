using Application.Features.CreditCardInfo.Dtos;
using Application.Features.Payments.Commands.CreatePayment;
using Application.Services.PosService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Payments.Rules
{
    public class PaymentBusinessRules
    {
        IPaymentRepository _paymentRepository;
        IPosService _posService;
        IMediator mediator;

        public PaymentBusinessRules(IPaymentRepository paymentRepository, IPosService posService, IMediator mediator)
        {
            _paymentRepository = paymentRepository;
            _posService = posService;
            this.mediator = mediator;
        }

        public async Task CheckIfPaymentIsSuccessful(CreditCardInfoDto creditCardInfo)
        {
            var result = await _posService.MakePayment(creditCardInfo);

            if (!result)
            {
                throw new BusinessException("Kredi kartı onaylanamadı!");
            }
        }
        public async Task MakePayment(CreatePaymentCommand command)
        {
            var result = await mediator.Send(command);

            if (result is null)
            {
                throw new BusinessException("Ödeme işlemi başarısız");
            }
        }
    }
}
