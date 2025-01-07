using DevFreela.Application.Commands.Users.CreateUser;
using DevFreela.Application.Commands.Users.CreateUserSkill;
using DevFreela.Application.Queries.Users.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(CreateUserCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id));
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpPost("{id}/skills")]
    public async Task<IActionResult> PostSkill(int id, CreateUserSkillCommand command)
    {
        command.Id = id;
        
        var result = await mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return NoContent();
    }
}