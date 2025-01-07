using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.Users.CreateUser;

public class CreateUserCommand : IRequest<ResultViewModel>
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public DateTime BirthDate { get; set; }
    
    public User ToEntity()
        => new (FullName, Email, BirthDate);
}