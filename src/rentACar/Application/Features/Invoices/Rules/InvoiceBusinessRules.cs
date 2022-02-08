using Application.Features.Invoices.Command.CreateInvoice;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices.Rules
{
    public  class InvoiceBusinessRules
    {
       IInvoiceRepository _invoiceRepository;
        IMediator _mediator;

        public InvoiceBusinessRules(IInvoiceRepository invoiceRepository, IMediator mediator)
        {
            _invoiceRepository = invoiceRepository;
            _mediator = mediator;
        }

        public async Task MakeInvoice(CreateInvoiceCommand command)
        {
            var result = await this._mediator.Send(command);

            if (result is null)
            {
                throw new BusinessException("Fatura eklenemedi");
            }
        }

    }
}

