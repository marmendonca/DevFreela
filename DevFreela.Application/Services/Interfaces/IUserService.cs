using DevFreela.API.Models;
using DevFreela.Application.Models;

namespace DevFreela.Application.Services.Interfaces;

public interface IUserService
{
    ResultViewModel<int> Insert(CreateUserInputModel inputModel);
    ResultViewModel<UserViewModel> GetById(int id);
    ResultViewModel InsertSkills(int id, UserSkillInputModel model);
    //ResultViewModel InsertProfilePicture(int id, IFormFile file);
}