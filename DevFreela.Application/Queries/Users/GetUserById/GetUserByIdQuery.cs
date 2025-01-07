using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.Users.GetUserById;

public class GetUserByIdQuery(int id) : IRequest<ResultViewModel<UserViewModel>>
{
    public int Id { get; set; } = id;
}