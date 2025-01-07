using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class CreateSkillInputModel
{
    public required string Description { get; set; }

    public Skill ToEntity()
        => new (Description);
}