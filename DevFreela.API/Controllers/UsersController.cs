using DevFreela.API.Models;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(DevFreelaDbContext devFreelaDbContext) : ControllerBase
{
    [HttpPost]
    public IActionResult Post(CreateUserInputModel inputModel)
    {
        var user = inputModel.ToEntity();
        
        devFreelaDbContext.Users.Add(user);
        devFreelaDbContext.SaveChanges();
        
        return NoContent();
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = devFreelaDbContext.Users
            .Include(item => item.Skills)
            .ThenInclude(item => item.Skill)
            .SingleOrDefault(u => u.Id == id);
        
        if (user == null)
            return NotFound();
        
        var userViewModel = UserViewModel.FromEntity(user);
        
        return Ok(userViewModel);
    }
    
    [HttpPost("{id}/skills")]
    public IActionResult PostSkill(int id, UserSkillInputModel inputModel)
    {
        var  userSkill = inputModel.SkillIds.Select(skillId => new UserSkill(id, skillId)).ToList();
        
        devFreelaDbContext.UserSkills.AddRange(userSkill);
        devFreelaDbContext.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPut("{id}/profile-picture")]
    public IActionResult PostProfilePicture(int id, IFormFile file)
    {
        var description = $"File: {file.FileName}, Size: {file.Length}";
        return Ok(description);
    }
}