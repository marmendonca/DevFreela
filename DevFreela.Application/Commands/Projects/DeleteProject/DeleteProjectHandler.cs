using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.SetAsDeleted();
        
        await projectRepository.UpdateAsync(project);
        
        return ResultViewModel.Success();
    }
}