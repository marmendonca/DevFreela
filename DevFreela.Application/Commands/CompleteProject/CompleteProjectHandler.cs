using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.CompleteProject;

public class CompleteProjectHandler(DevFreelaDbContext context) : IRequestHandler<CompleteProjectCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await context.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
        
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        project.Complete();
        
        context.Projects.Update(project);
        await context.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}