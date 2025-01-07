using DevFreela.Application.Models;

namespace DevFreela.Application.Services.Interfaces;

public interface IProjectService
{
    ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 5);
    ResultViewModel<ProjectViewModel> GetById(int id);
    ResultViewModel<int> Insert(CreateProjectInputModel model);
    ResultViewModel Update(UpdateProjectInputModel inputModel);
    ResultViewModel Delete(int id);
    ResultViewModel Start(int id);
    ResultViewModel Complete(int id);
    ResultViewModel InsertComment(CreateProjectCommentInputModel model);
}