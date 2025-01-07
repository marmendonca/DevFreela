using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
{
    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetDetailsByIdAsync(request.Id);
        
        if (project is null)
            return ResultViewModel<ProjectViewModel>.Error("Project not found");
        
        var model = ProjectViewModel.FromEntity(project);

        return ResultViewModel<ProjectViewModel>.Success(model);
    }
}