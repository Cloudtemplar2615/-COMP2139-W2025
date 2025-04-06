using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2139_ICE.Data;
using COMP2139_ICE.Areas.ProjectManagement.Models;

namespace COMP2139_ICE.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("ProjectManagement/[controller]/[action]")]
    public class ProjectTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProjectTasksController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.ProjectTasks.Include(t => t.Project).ToListAsync();
            return View(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["ProjectId"] = new SelectList(await _context.Projects.ToListAsync(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTask task)
        {
            Console.WriteLine(">>> HITTING POST CREATE");

            
            ModelState.Remove("Project");

            if (!ModelState.IsValid)
            {
                Console.WriteLine(">>> MODEL INVALID");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"ERROR in {state.Key}: {error.ErrorMessage}");
                    }
                }

                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
                return View(task);
            }

            task.DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc);
            _context.Add(task);
            await _context.SaveChangesAsync();
            Console.WriteLine(">>> TASK CREATED");
            return RedirectToAction(nameof(Index));
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null) return NotFound();
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(ProjectTask task)
        {
            ModelState.Remove("Project");

            if (!ModelState.IsValid)
            {
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
                return View("Edit", task);
            }

            task.DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc);
            _context.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.ProjectTasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
            return task == null ? NotFound() : View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var task = await _context.ProjectTasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
            return task == null ? NotFound() : View(task);
        }
        [HttpGet]
        public async Task<IActionResult> ByProject(int id)
        {
            var tasks = await _context.ProjectTasks
                .Include(t => t.Project)
                .Where(t => t.ProjectId == id)
                .ToListAsync();

            ViewBag.ProjectName = tasks.FirstOrDefault()?.Project?.Name ?? "Unknown";
            return View("ByProject", tasks);
        }

    }
}



