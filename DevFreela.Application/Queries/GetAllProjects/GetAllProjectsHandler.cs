using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsHandler(IProjectRepository projectRepository) : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
{
    public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await projectRepository.GetAllAsync();

        var models = projects.Select(ProjectItemViewModel.FromEntity).ToList();
        
        return ResultViewModel<List<ProjectItemViewModel>>.Success(models);
    }
}