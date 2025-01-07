using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectHandler(DevFreelaDbContext context) : IRequestHandler<UpdateProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.Update(request.Title, request.Description, request.TotalCost);
        
        context.Projects.Update(project);
        await context.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}