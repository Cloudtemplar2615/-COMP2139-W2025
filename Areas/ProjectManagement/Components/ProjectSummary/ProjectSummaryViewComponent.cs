using Microsoft.AspNetCore.Mvc;
using COMP2139_ICE.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace COMP2139_ICE.Areas.ProjectManagement.Components.ProjectSummary
{
    public class ProjectSummaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ProjectSummaryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int projectId)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectTasks)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return View("Error"); // Optional: handle missing project
            }

            return View(project);
        }
    }
}