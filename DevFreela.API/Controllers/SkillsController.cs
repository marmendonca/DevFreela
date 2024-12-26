using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillsController(DevFreelaDbContext devFreelaDbContext) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var skills = devFreelaDbContext.Skills.ToList();
        
        return Ok(skills.Select(SkillViewModel.FromEntity));
    }
    
    [HttpPost]
    public IActionResult Post(CreateSkillInputModel inputModel)
    {
        var skill = inputModel.ToEntity();

        devFreelaDbContext.Skills.Add(skill);
        devFreelaDbContext.SaveChanges();
        
        return NoContent();
    }
}