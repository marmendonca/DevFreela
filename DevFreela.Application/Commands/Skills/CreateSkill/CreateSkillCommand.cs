using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.Skills.CreateSkill;

public class CreateSkillCommand : IRequest<ResultViewModel>
{
    public required string Description { get; set; }

    public Skill ToEntity()
        => new (Description);
}