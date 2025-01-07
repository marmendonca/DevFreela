using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.DeleteProject;

public class DeleteProjectHandler(DevFreelaDbContext context) : IRequestHandler<DeleteProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.SetAsDeleted();
        
        context.Projects.Update(project);
        await context.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}