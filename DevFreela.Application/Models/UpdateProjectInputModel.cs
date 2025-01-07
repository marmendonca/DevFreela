namespace DevFreela.Application.Models;

public class UpdateProjectInputModel
{
    public int IdProject { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public decimal TotalCost { get; set; }
}