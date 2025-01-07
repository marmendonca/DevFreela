using DevFreela.Application.Models;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillsController(ISkillService skillService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var results = skillService.GetAll();
        
        return Ok(results);
    }
    
    [HttpPost]
    public IActionResult Post(CreateSkillInputModel inputModel)
    {
        var result = skillService.Insert(inputModel);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return NoContent();
    }
}