using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectCommand : IRequest<ResultViewModel<int>>
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int IdClient { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }
    
    public Project ToEntity()
        => new Project(Title, Description, IdClient, IdFreelancer, TotalCost);
}