using MediatR;

namespace DevFreela.Application.Notifications.Project.Created;

public class ProjectCreatedNotification(string title) : INotification
{
    public string Title { get; set; } = title;
}