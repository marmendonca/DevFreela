using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Repositories;

public class ProjectRepository(DevFreelaDbContext context) : IProjectRepository
{
    public async Task<List<Project>> GetAllAsync()
    {
        return await context
            .Projects
            .Include(item => item.Client)
            .Include(item => item.Freelancer)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await context
            .Projects
            .SingleOrDefaultAsync(item => item.Id == id);
    }

    public async Task<Project?> GetDetailsByIdAsync(int id)
    {
        return await context
            .Projects
            .Include(item => item.Client)
            .Include(item => item.Freelancer)
            .Include(item => item.Comments)
            .SingleOrDefaultAsync(item => item.Id == id);
    }

    public async Task<int> AddAsync(Project project)
    {
        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();

        return project.Id;
    }

    public async Task UpdateAsync(Project project)
    {
        context.Projects.Update(project);
        await context.SaveChangesAsync();
    }

    public async Task AddCommentAsync(ProjectComment projectComment)
    {
        await context.ProjectComments.AddAsync(projectComment);
        await context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id)
    {
        return context.Projects.AnyAsync(item => item.Id == id);
    }
}