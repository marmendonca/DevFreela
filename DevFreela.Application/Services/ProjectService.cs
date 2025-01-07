using DevFreela.Application.Models;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;

public class ProjectService(DevFreelaDbContext context) : IProjectService
{
    public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search, int page, int size)
    {
        var projects = context
            .Projects
            .Include(item => item.Client)
            .Include(item => item.Freelancer)
            .Where(p => !p.IsDeleted && (string.IsNullOrEmpty(search) || p.Title.Contains(search) || p.Description.Contains(search)))
            .Skip(page * size)
            .Take(size)
            .ToList();

        var models = projects.Select(ProjectItemViewModel.FromEntity).ToList();
        
        return ResultViewModel<List<ProjectItemViewModel>>.Success(models);
    }

    public ResultViewModel<ProjectViewModel> GetById(int id)
    {
        var project = context
            .Projects
            .Include(item => item.Client)
            .Include(item => item.Freelancer)
            .Include(item => item.Comments)
            .SingleOrDefault(item => item.Id == id);
        
        if (project is null)
            return ResultViewModel<ProjectViewModel>.Error("Project not found");
        
        var model = ProjectViewModel.FromEntity(project);

        return ResultViewModel<ProjectViewModel>.Success(model);
    }

    public ResultViewModel<int> Insert(CreateProjectInputModel model)
    {
        var project = model.ToEntity();
        
        context.Projects.Add(project);
        context.SaveChanges();

        return ResultViewModel<int>.Success(project.Id);
    }

    public ResultViewModel Update(UpdateProjectInputModel inputModel)
    {
        var project = context.Projects.SingleOrDefault(p => p.Id == inputModel.IdProject);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
        context.Projects.Update(project);
        context.SaveChanges();
        
        return ResultViewModel.Success();
    }

    public ResultViewModel Delete(int id)
    {
        var project = context.Projects.SingleOrDefault(p => p.Id == id);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.SetAsDeleted();
        
        context.Projects.Update(project);
        context.SaveChanges();
        
        return ResultViewModel.Success();
    }

    public ResultViewModel Start(int id)
    {
        var project = context.Projects.SingleOrDefault(p => p.Id == id);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.Start();
        
        context.Projects.Update(project);
        context.SaveChanges();
        
        return ResultViewModel.Success();
    }

    public ResultViewModel Complete(int id)
    {
        var project = context.Projects.SingleOrDefault(p => p.Id == id);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.Complete();
        
        context.Projects.Update(project);
        context.SaveChanges();
        
        return ResultViewModel.Success();
    }

    public ResultViewModel InsertComment(CreateProjectCommentInputModel model)
    {
        var project = context.Projects.SingleOrDefault(p => p.Id == model.IdProject);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        var projectComment = model.ToEntity();
        
        context.ProjectComments.Add(projectComment);
        context.SaveChanges();
        
        return ResultViewModel.Success();
    }
}