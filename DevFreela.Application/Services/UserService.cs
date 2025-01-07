using DevFreela.API.Models;
using DevFreela.Application.Models;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;

public class UserService(DevFreelaDbContext context) : IUserService
{
    public ResultViewModel<int> Insert(CreateUserInputModel inputModel)
    {
        var user = inputModel.ToEntity();
        
        context.Users.Add(user);
        context.SaveChanges();
        
        return ResultViewModel<int>.Success(user.Id);
    }

    public ResultViewModel<UserViewModel> GetById(int id)
    {
        var user = context.Users
            .Include(item => item.Skills)
            .ThenInclude(item => item.Skill)
            .SingleOrDefault(u => u.Id == id);

        if (user == null)
            return ResultViewModel<UserViewModel>.Error("Usuário não encontrado");
        
        var userViewModel = UserViewModel.FromEntity(user);
        
        return ResultViewModel<UserViewModel>.Success(userViewModel);
    }

    public ResultViewModel InsertSkills(int id, UserSkillInputModel model)
    {
        var  userSkill = model.SkillIds.Select(skillId => new UserSkill(id, skillId)).ToList();
        
        context.UserSkills.AddRange(userSkill);
        context.SaveChanges();
        
        return ResultViewModel.Success();
    }
}