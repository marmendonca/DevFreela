using DevFreela.Application.Commands.Skills.CreateSkill;
using DevFreela.Application.Queries.Skills.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await mediator.Send(new GetAllSkillsQuery());
        
        return Ok(results);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(CreateSkillCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return NoContent();
    }
}