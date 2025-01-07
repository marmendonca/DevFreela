using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task<Project?> GetDetailsByIdAsync(int id);
    Task<int> AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task AddCommentAsync(ProjectComment projectComment);
    Task<bool> ExistsAsync(int id);
}