using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class SkillViewModel
{
    public string Description { get; set; }
    
    public SkillViewModel(string description)
    {
        Description = description;
    }
    
    public SkillViewModel(int id, string description)
    {
        Description = description;
    }

    public static SkillViewModel FromEntity(Skill skill)
        => new(skill.Id, skill.Description);
}