using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_ICE.Data;
using COMP2139_ICE.Areas.ProjectManagement.Models;
using System.Text.Json;


namespace COMP2139_ICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectCommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetComments(int projectId)
        {
            var comments = await _context.ProjectComments
                .Where(c => c.ProjectId == projectId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();

            return Ok(comments);
        }


        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] JsonElement json)
        {
            try
            {
                // Extract raw data manually
                var content = json.GetProperty("content").GetString();
                var projectId = json.GetProperty("projectId").GetInt32();

                // Validate manually
                if (string.IsNullOrWhiteSpace(content) || projectId == 0)
                {
                    return BadRequest(new { error = "Content and ProjectId are required." });
                }

                var comment = new ProjectComment
                {
                    Content = content,
                    ProjectId = projectId,
                    CreatedDate = DateTime.UtcNow
                };

                _context.ProjectComments.Add(comment);
                await _context.SaveChangesAsync();

                return Ok(comment);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ JSON PARSE ERROR: " + ex.Message);
                return BadRequest(new { error = "Invalid request payload." });
            }
        }
    }
}