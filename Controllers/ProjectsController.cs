using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_ICE.Data;
using COMP2139_ICE.Models;

namespace COMP2139_ICE.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            project.StartDate = DateTime.SpecifyKind(project.StartDate, DateTimeKind.Utc);
            project.EndDate = DateTime.SpecifyKind(project.EndDate, DateTimeKind.Utc);

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectTasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return NotFound();

            return View(project);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Project project)
        {
            project.StartDate = DateTime.SpecifyKind(project.StartDate, DateTimeKind.Utc);
            project.EndDate = DateTime.SpecifyKind(project.EndDate, DateTimeKind.Utc);

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
