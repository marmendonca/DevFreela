using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.Skills.GetAllSkills;

public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<IEnumerable<SkillViewModel>>>
{
    public async Task<ResultViewModel<IEnumerable<SkillViewModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}