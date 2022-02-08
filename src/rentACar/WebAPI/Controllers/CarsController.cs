using Application.Features.Cars.Commands;
using Application.Features.Cars.Commands.CreateCar;
using Application.Features.Cars.Commands.DeleteCar;
using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.Cars.Queries;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCarCommand createCarCommand)
        {
            var result = await Mediator.Send(createCarCommand);
            return Created("", result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCarCommand updateCarCommand)
        {
            var result = await Mediator.Send(updateCarCommand);
            return Ok(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCarCommand deleteCarCommand)
        {
            var result = await Mediator.Send(deleteCarCommand);
            return Ok(result);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var query = new GetCarListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
