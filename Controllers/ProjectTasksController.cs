using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_ICE.Data;
using COMP2139_ICE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace COMP2139_ICE.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTasks
        public async Task<IActionResult> Index()
        {
            var tasks = _context.ProjectTasks.Include(t => t.Project);
            return View(await tasks.ToListAsync());
        }

        // GET: ProjectTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.ProjectTasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null) return NotFound();

            return View(task);
        }

        // GET: ProjectTasks/Create
        [HttpGet("ProjectTasks/Create")]
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            return View();
        }


        // POST: ProjectTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            return View(task);
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null) return NotFound();

            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            return View(task);
        }

        // POST: ProjectTasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectTask task)
        {
            if (id != task.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(task.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            return View(task);
        }

        // GET: ProjectTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.ProjectTasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null) return NotFound();

            return View(task);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
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

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTasks.Any(e => e.Id == id);
        }
    }
}
