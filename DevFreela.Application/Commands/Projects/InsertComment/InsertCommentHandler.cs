using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertComment;

public class InsertCommentHandler(IProjectRepository projectRepository) : IRequestHandler<InsertCommentCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.IdProject);
        if (project is null)
            return ResultViewModel.Error("Project not found");
        
        var projectComment = request.ToEntity();

        await projectRepository.AddCommentAsync(projectComment);
        
        return ResultViewModel.Success();
    }
}