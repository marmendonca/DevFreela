using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.IdProject);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.Update(request.Title, request.Description, request.TotalCost);
        
        await projectRepository.UpdateAsync(project);
        
        return ResultViewModel.Success();
    }
}