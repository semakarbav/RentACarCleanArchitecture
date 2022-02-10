using Application.Features.Users.Commands.CreateUSer;
using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Queries.GetListUser;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var query = new GetListUserQuery();
            query.PageRequest = pageRequest;
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand createUserCommand)
        {
            var result = await Mediator.Send(createUserCommand);
            return Created("", result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var result = await Mediator.Send(loginUserCommand);
            return Created("", result);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        //{
        //    UpdatedUserDto result = await Mediator!.Send(updateUserCommand);
        //    return Ok(result);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUserCommand)
        //{
        //    DeletedUserDto result = await Mediator!.Send(deleteUserCommand);
        //    return Ok(result);
        //    }
        //}
    }
}
