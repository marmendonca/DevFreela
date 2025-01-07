using DevFreela.Application.Models;
using DevFreela.Application.Notifications.Project.Created;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectHandler(DevFreelaDbContext context, IMediator mediator) : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();
        
        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();
        
        await mediator.Publish(new ProjectCreatedNotification(project.Title));

        return ResultViewModel<int>.Success(project.Id);
    }
}