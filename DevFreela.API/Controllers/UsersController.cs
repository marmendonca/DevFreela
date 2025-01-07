using DevFreela.API.Models;
using DevFreela.Application.Models;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public IActionResult Post(CreateUserInputModel inputModel)
    {
        var result = userService.Insert(inputModel);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = userService.GetById(id);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return Ok(result);
    }
    
    [HttpPost("{id}/skills")]
    public IActionResult PostSkill(int id, UserSkillInputModel inputModel)
    {
        
        var result = userService.InsertSkills(id, inputModel);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        
        return NoContent();
    }
    
    [HttpPut("{id}/profile-picture")]
    public IActionResult PostProfilePicture(int id, IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";
        return Ok(description);
    }
}