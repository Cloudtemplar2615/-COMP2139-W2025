using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2139_ICE.Data;
using COMP2139_ICE.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace COMP2139_ICE.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("ProjectManagement/[controller]/[action]")]
    public class ProjectTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public ProjectTasksController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: /projecttasks or /projecttasks/index?projectId=...
        [HttpGet("")]
        [HttpGet("index")]
        public async Task<IActionResult> Index(int? projectId)
        {
            var tasksQuery = _context.ProjectTasks.Include(t => t.Project).AsQueryable();
            if (projectId.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.ProjectId == projectId.Value);
            }
            return View(await tasksQuery.ToListAsync());
        }
        
        // GET: /projecttasks/search?searchTerm=...
        [HttpGet("search")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            ViewData["SearchTerm"] = searchTerm;
            var tasks = string.IsNullOrEmpty(searchTerm)
                ? await _context.ProjectTasks.Include(t => t.Project).ToListAsync()
                : await _context.ProjectTasks.Include(t => t.Project)
                    .Where(t => t.Title.Contains(searchTerm) || t.Description.Contains(searchTerm))
                    .ToListAsync();
            ViewData["SearchPerformed"] = true;
            return View("Index", tasks);
        }
        
        // GET: /projecttasks/create
        [HttpGet("create")]
        public IActionResult Create(int? projectId)
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", projectId);
            return View();
        }
        
        // POST: /projecttasks/create
        [HttpPost("create")]
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
        
        // GET: /projecttasks/edit/5
        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null) return NotFound();
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", task.ProjectId);
            return View(task);
        }
        
        // POST: /projecttasks/edit
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectTask task)
        {
            if (!ProjectTaskExists(task.Id))
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.ProjectTasks.Update(task);
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
        
        // GET: /projecttasks/details/5
        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var task = await _context.ProjectTasks.Include(t => t.Project)
                         .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null) return NotFound();
            return View(task);
        }
        
        // GET: /projecttasks/delete/5
        [HttpGet("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.ProjectTasks.Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null) return NotFound();
            return View(task);
        }
        
        // POST: /projecttasks/delete
        [HttpPost("delete")]
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


