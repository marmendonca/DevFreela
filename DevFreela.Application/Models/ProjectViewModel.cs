using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class ProjectViewModel
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; set; }
    public int IdClient { get; set; }
    public int IdFreelancer { get; set; }
    public string ClientName { get; set; }
    public string FreelancerName { get; set; }
    public decimal TotalCost { get; set; }
    public List<string> Comments { get; set; }

    public ProjectViewModel(int id, string title, string description, int idClient, int idFreelancer, string clientName, string freelancerName, decimal totalCost, List<ProjectComment> comments)
    {
        Id = id;
        Title = title;
        Description = description;
        IdClient = idClient;
        IdFreelancer = idFreelancer;
        ClientName = clientName;
        FreelancerName = freelancerName;
        TotalCost = totalCost;
        Comments = comments?.Count > 0 ? comments.Select(item => item.Content).ToList() : [];
    }
    
    public static ProjectViewModel FromEntity(Project project)
        => new (
            project.Id, 
            project.Title, 
            project.Description, 
            project.IdClient, 
            project.IdFreelancer, 
            project.Client.FullName, 
            project.Freelancer.FullName, 
            project.TotalCost, 
            project.Comments);
}