using Microsoft.AspNetCore.Mvc;
using COMP2139_ICE.Models;

namespace COMP2139_ICE.Controllers;

public class ProjectsController : Controller
{
    private static List<Project> _projects = new List<Project>();

    public IActionResult Index()
    {
        return View(_projects);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Project project)
    {
        _projects.Add(project);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        if (project == null) return NotFound();
        return View(project);
    }
}
