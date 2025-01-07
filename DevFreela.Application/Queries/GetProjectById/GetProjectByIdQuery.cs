using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQuery(int id) : IRequest<ResultViewModel<ProjectViewModel>>
{
    public int Id { get; set; } = id;
}