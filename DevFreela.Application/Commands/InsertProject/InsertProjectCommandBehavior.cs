using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.InsertProject;

public class InsertProjectCommandBehavior(DevFreelaDbContext context) : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
    {
        var client = await context.Users.SingleOrDefaultAsync(u => u.Id == request.IdClient);
        var freelancer = await context.Users.SingleOrDefaultAsync(u => u.Id == request.IdFreelancer);
        
        if (client == null || freelancer == null)
            return ResultViewModel<int>.Error("Client or freelancer does not exist");
        
        return await next();
    }
}