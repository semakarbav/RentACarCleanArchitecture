using Application.Features.Rentals.Commands.CreateRental;
using Application.Features.Rentals.Commands.CreateRental.CreateEndRentalForCorporateCustomer;
using Application.Features.Rentals.Commands.CreateRental.CreateEndRentalForIndividualCustomer;
using Application.Features.Rentals.Commands.CreateRental.CreateRentalForCorporateCustomer;
using Application.Features.Rentals.Commands.DeleteRental;
using Application.Features.Rentals.Commands.UpdateRental;
using Application.Features.Rentals.Queries.GetRentalList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : BaseController
    {
        [HttpPost("rentforindividualcustomer")]
        public async Task<IActionResult> RentForIndividualCustomer([FromBody] RentForIndividualCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }

        [HttpPost("rentforcorporatecustomer")]
        public async Task<IActionResult> RentForCorporateCustomer([FromBody] RentForCorporateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPost("rentendforcorporatecustomer")]
        public async Task<IActionResult> RentEndForCorporateCustomer([FromBody] CreateEndRentalCommandForCorporateCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPost("rentendforindividualcustomer")]
        public async Task<IActionResult> RentEndForIndividualCustomer([FromBody] CreateEndRentalCommandForIndividualCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRentalCommand updateRentalCommand)
        {
            var result = await Mediator.Send(updateRentalCommand);
            return Ok(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteRentalCommand deleteRentalCommand)
        {
            var result = await Mediator.Send(deleteRentalCommand);
            return Ok(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var query = new GetRentalListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
