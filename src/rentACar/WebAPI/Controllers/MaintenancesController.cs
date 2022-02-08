using Application.Features.Maintenances.Commands.DeleteMaintenance;
using Application.Features.Maintenances.Commands.UpdateMaintenance;
using Application.Features.Maintenances.Queries.GetMaintenanceList;
using Application.Features.Maintenenaces.Commands.CreateMaintenance;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenancesController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddMaintenance([FromBody] CreateMaintenanceCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateMaintenanceCommand updateMaintenanceCommand)
        {
            var result = await Mediator.Send(updateMaintenanceCommand);
            return Ok(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteMaintenanceCommand deleteMaintenanceCommand)
        {
            var result = await Mediator.Send(deleteMaintenanceCommand);
            return Ok(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
        {
            var query = new GetMaintenanceListQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
