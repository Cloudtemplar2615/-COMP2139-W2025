using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_ICE.Data;

namespace COMP2139_ICE.Components
{
    public class ProjectSummaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ProjectSummaryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projectCount = await _context.Projects.CountAsync();
            var taskCount = await _context.ProjectTasks.CountAsync();
            return View(new { projectCount, taskCount });
        }
    }
}