using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class UserViewModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public List<string> Skills { get; set; }
    
    public UserViewModel(string fullName, string email, DateTime birthDate, List<string> skills)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Skills = skills;
    }

    public static UserViewModel FromEntity(User user)
        => new(user.FullName, user.Email, user.BirthDate, user.Skills.Select(s => s.Skill.Description).ToList());
}