using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectHandler(IProjectRepository projectRepository) : IRequestHandler<StartProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.Id);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.Start();
        
        await projectRepository.UpdateAsync(project);
        
        return ResultViewModel.Success();
    }
}