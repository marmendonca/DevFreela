using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.CompleteProject;

public class CompleteProjectHandler(IProjectRepository projectRepository) : IRequestHandler<CompleteProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);
        
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.Complete();
        
        await projectRepository.UpdateAsync(project);
        
        return ResultViewModel.Success();
    }
}