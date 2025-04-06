using Microsoft.AspNetCore.Mvc;
using COMP2139_ICE.Data;
using COMP2139_ICE.Areas.ProjectManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace COMP2139_ICE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: / or /Home/Index – This is your landing page.
        [AllowAnonymous]
        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
        
        // General search action for both Projects and ProjectTasks
        [HttpGet("search")]
        public IActionResult Search(string searchTerm, string category)
        {
            ViewData["SearchTerm"] = searchTerm;
            if (category == "ProjectTask")
            {
                var tasks = string.IsNullOrEmpty(searchTerm)
                    ? _context.ProjectTasks.Include(t => t.Project).ToList()
                    : _context.ProjectTasks.Include(t => t.Project)
                        .Where(t => t.Title.Contains(searchTerm) || t.Description.Contains(searchTerm))
                        .ToList();
                return View("SearchResults", tasks);
            }
            else
            {
                var projects = string.IsNullOrEmpty(searchTerm)
                    ? _context.Projects.ToList()
                    : _context.Projects.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                        .ToList();
                return View("SearchResults", projects);
            }
        }
        
        // 404 NotFound page action
        [HttpGet("notfound")]
        public IActionResult NotFoundPage()
        {
            return View("NotFound");
        }
        
        // (Optional) Error page action
        [HttpGet("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
