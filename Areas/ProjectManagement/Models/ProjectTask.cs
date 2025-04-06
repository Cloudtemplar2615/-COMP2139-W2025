namespace COMP2139_ICE.Areas.ProjectManagement.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class ProjectTask
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    public bool IsComplete { get; set; }

    [Required]
    public int ProjectId { get; set; }

    public Project Project { get; set; } = default!;
}

