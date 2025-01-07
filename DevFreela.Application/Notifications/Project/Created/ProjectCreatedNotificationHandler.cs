using MediatR;

namespace DevFreela.Application.Notifications.Project.Created;

public class ProjectCreatedNotificationHandler : INotificationHandler<ProjectCreatedNotification>
{
    public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Project created: {notification.Title}");
        return Task.CompletedTask;
    }
}