using DevFreela.Application.Models;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services;

public class SkillService(DevFreelaDbContext context) : ISkillService
{
    public ResultViewModel<List<SkillViewModel>> GetAll()
    {
        var skills = context.Skills.ToList();
        
        var skillsViewModel = skills
            .Select(s => new SkillViewModel(s.Id, s.Description))
            .ToList();
        
        return ResultViewModel<List<SkillViewModel>>.Success(skillsViewModel);
    }

    public ResultViewModel<int> Insert(CreateSkillInputModel inputModel)
    {
        var skill = inputModel.ToEntity();

        context.Skills.Add(skill);
        context.SaveChanges();
        
        return ResultViewModel<int>.Success(skill.Id);
    }
}