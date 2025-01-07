using DevFreela.Application.Models;
using DevFreela.Application.Notifications.Project.Created;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectHandler(IProjectRepository projectRepository, IMediator mediator) : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();
        
        await projectRepository.AddAsync(project);
        
        await mediator.Publish(new ProjectCreatedNotification(project.Title));

        return ResultViewModel<int>.Success(project.Id);
    }
}