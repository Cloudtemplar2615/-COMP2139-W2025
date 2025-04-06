using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_ICE.Areas.ProjectManagement.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required] public string Name { get; set; } = string.Empty;

        [Required] public string Description { get; set; } = string.Empty;

        [DataType(DataType.Date)] public DateTime StartDate { get; set; }

        [DataType(DataType.Date)] public DateTime EndDate { get; set; }

        public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    }
}


