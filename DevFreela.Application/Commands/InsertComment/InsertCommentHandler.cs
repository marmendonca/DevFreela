using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertComment;

public class InsertCommentHandler(DevFreelaDbContext context) : IRequestHandler<InsertCommentCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        var projectComment = request.ToEntity();
        
        context.ProjectComments.Add(projectComment);
        await context.SaveChangesAsync();
        
        return ResultViewModel.Success();
    }
}