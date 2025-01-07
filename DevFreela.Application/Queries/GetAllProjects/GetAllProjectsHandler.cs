using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsHandler(DevFreelaDbContext context) : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await context
            .Projects
            .Include(item => item.Client)
            .Include(item => item.Freelancer)
            .Where(p => !p.IsDeleted && (string.IsNullOrEmpty(request.Search) || p.Title.Contains(request.Search) || p.Description.Contains(request.Search)))
            .Skip(request.Page * request.Size)
            .Take(request.Size)
            .ToListAsync();

        var models = projects.Select(ProjectItemViewModel.FromEntity).ToList();
        
        return ResultViewModel<List<ProjectItemViewModel>>.Success(models);
    }
}