using Application.Features.Invoices.Command.CreateInvoice;
using Application.Features.Invoices.Queries.GetInvoiceList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : BaseController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetListInvoices([FromQuery] PageRequest pageRequest)
        {
            var query = new GetInvoiceListQuery();
            query.PageRequest = pageRequest;

            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateInvoiceCommand invoiceBrandCommand)
        {
            var result = await Mediator.Send(invoiceBrandCommand);
            return Created("", result);
        }
    }
}
