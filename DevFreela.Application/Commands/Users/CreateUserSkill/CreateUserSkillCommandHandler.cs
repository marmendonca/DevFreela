using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Users.CreateUserSkill;

public class CreateUserSkillCommandHandler : IRequestHandler<CreateUserSkillCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(CreateUserSkillCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}