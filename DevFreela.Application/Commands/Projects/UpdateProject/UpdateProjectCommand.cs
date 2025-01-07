using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest<ResultViewModel>
{
    public int IdProject { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public decimal TotalCost { get; set; }
}