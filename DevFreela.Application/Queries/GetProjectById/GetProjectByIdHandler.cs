using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdHandler(DevFreelaDbContext context) : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
{
    public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await context
            .Projects
            .Include(item => item.Client)
            .Include(item => item.Freelancer)
            .Include(item => item.Comments)
            .SingleOrDefaultAsync(item => item.Id == request.Id);
        
        if (project is null)
            return ResultViewModel<ProjectViewModel>.Error("Project not found");
        
        var model = ProjectViewModel.FromEntity(project);

        return ResultViewModel<ProjectViewModel>.Success(model);
    }
}