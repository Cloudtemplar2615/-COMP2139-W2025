namespace COMP2139_ICE.Areas.ProjectManagement.Models;

public class ProjectTask
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }

    public bool IsComplete { get; set; }

    // Foreign key
    public int ProjectId { get; set; }

    // Navigation property
    public Project? Project { get; set; }
}
