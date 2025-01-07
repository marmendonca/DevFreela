using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Skills.CreateSkill;

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}