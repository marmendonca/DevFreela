using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.InsertComment;

public class InsertCommentCommand : IRequest<ResultViewModel>
{
    public int IdProject { get; set; }
    public int IdUser { get; set; }
    public required string Content { get; set; }
    
    public ProjectComment ToEntity()
        => new (Content, IdProject, IdUser);
}