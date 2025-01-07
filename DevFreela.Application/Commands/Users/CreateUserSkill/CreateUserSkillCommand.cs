using System.Text.Json.Serialization;
using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.Users.CreateUserSkill;

public class CreateUserSkillCommand : IRequest<ResultViewModel>
{
    public int[] SkillIds { get; set; } = [];
    [JsonIgnore]
    public int Id { get; set; }
}