using DevFreela.API.Models;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectsController(IOptions<FreelanceTotalCostConfig> options, DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult Get(string search = "", int page = 0, int size = 5)
    {
        var projects = _dbContext
            .Projects
            .Include(item => item.Client)
            .Include(item => item.Freelancer)
            .Where(p => !p.IsDeleted && (string.IsNullOrEmpty(search) || p.Title.Contains(search) || p.Description.Contains(search)))
            .Skip(page * size)
            .Take(size)
            .ToList();

        var models = projects.Select(ProjectItemViewModel.FromEntity).ToList();
        
        return Ok(models);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var project = _dbContext
            .Projects
            .Include(item => item.Client)
            .Include(item => item.Freelancer)
            .SingleOrDefault(item => item.Id == id);

        var model = ProjectViewModel.FromEntity(project);
        
        return Ok(model);
    }
    
    [HttpPost]
    public IActionResult Post(CreateProjectInputModel inputModel)
    {
        var project = inputModel.ToEntity();
        
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
        
        return CreatedAtAction(nameof(GetById), new { id = 1 }, inputModel);
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel)
    {
        inputModel.IdProject = id;
            
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        
        project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        
        project.SetAsDeleted();
        
        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPut("{id}/start")]
    public IActionResult Start(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        
        project.Start();
        
        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPut("{id}/complete")]
    public IActionResult Complete(int id)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        
        project.Complete();
        
        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();
        
        return NoContent();
    }
    
    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateProjectCommentInputModel inputModel)
    {
        var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        
        var projectComment = inputModel.ToEntity();
        
        _dbContext.ProjectComments.Add(projectComment);
        _dbContext.SaveChanges();
        
        return NoContent();
    }
}