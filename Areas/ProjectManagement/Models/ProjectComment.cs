using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace COMP2139_ICE.Areas.ProjectManagement.Models
{
    public class ProjectComment
    {
        [Key] 
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        [BindNever]
        [JsonIgnore]
        public Project Project { get; set; }
    }
}