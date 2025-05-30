using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers;

public class PersonController : Controller
{
    private readonly ILogger<PersonController> _logger;
    private readonly AppDbContext _context;

    public PersonController(ILogger<PersonController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var peopleData = _context.People.ToList();
        return View(peopleData);
    }

    public IActionResult Upsert(int? id)
    {
        if (id == null || id == 0)
        {
            return View(new People() { Id = 0, Age = 18, Name = "" });
        }
        else
        {
            var person = _context.People.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
    }

    [HttpPost]
    public IActionResult Upsert(People person)
    {
        //if (ModelState.IsValid)
        //{
        if (person.Id == 0)
        {
            var lastId = _context.People.OrderByDescending(x => x.Id).FirstOrDefault()?.Id ?? 0;
            person.Id = lastId + 1;
            _context.People.Add(person);
        }
        else
        {
            _context.People.Update(person);
        }
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
        //}

        //return View(person);
    }

}
