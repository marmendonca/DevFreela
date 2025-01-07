using DevFreela.Application.Models;

namespace DevFreela.Application.Services.Interfaces;

public interface ISkillService
{
    ResultViewModel<List<SkillViewModel>> GetAll();
    ResultViewModel<int> Insert(CreateSkillInputModel inputModel);
}