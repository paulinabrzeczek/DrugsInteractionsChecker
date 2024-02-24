using Microsoft.AspNetCore.Mvc;
using InterakcjeMiedzyLekami.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InterakcjeMiedzyLekami.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Strona główna";
            List<Pharmaceutical> pharmaceuticals = _context.Pharmaceuticals.ToList();
            return View(pharmaceuticals);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() 
        {
            ViewData["Title"] = "Dodaj lek";
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Pharmaceutical pharmaceutical)
        {
            ViewData["Title"] = "Dodaj lek";
            _context.Pharmaceuticals.Add(pharmaceutical);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            ViewData["Title"] = "Edytuj lek";
            Pharmaceutical pharmaceutical = _context.Pharmaceuticals.Find(id);
            return View(pharmaceutical);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Pharmaceutical pharmaceutical)
        {
            ViewData["Title"] = "Edytuj lek";
            _context.Pharmaceuticals.Update(pharmaceutical);
            _context.SaveChanges();
            return RedirectToAction("Index");    
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Pharmaceutical pharmaceutical = _context.Pharmaceuticals.Find(id);
            _context.Pharmaceuticals.Remove(pharmaceutical);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}