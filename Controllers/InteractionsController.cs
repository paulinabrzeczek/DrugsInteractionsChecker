using Microsoft.AspNetCore.Mvc;
using InterakcjeMiedzyLekami.Services.Interfaces;
using InterakcjeMiedzyLekami.Models;

namespace InterakcjeMiedzyLekami.Controllers
{
    public class InteractionsController : Controller
    {
        private readonly IInteractions _interactions;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public InteractionsController( IConfiguration configuration, ApplicationDbContext context, IInteractions interactions)
        {
            _interactions = interactions;
            _configuration = configuration;
            _context = context;
        }

        public IActionResult GetPharmaceuticals()
        {
            ViewData["Title"] = "Interakcje z lekami";
            ViewData["description"] = "";
            var getPharmaceuticals = _interactions.GetPharmaceuticals();
            return View(getPharmaceuticals);
        }

        [HttpPost]
        public IActionResult GetPharmaceuticals(string pharmaceuticalId)
        {
            ViewData["Title"] = "Interakcje z lekami";
            ViewData["description"] = _interactions.CheckInteractions(pharmaceuticalId);
            var getPharmaceuticals = _interactions.GetPharmaceuticals();
            return View(getPharmaceuticals);
        }

        public IActionResult GetInteractions()
        {
            // Tutaj mozna dodawac nazwy stron wyswietlanie w zakladce
            ViewData["Title"] = "Interakcje z substancjami";
            ViewData["description"] = "";
            var getPharmaceuticalsSubstances = _interactions.GetPharmaceuticalsSubstances();
            return View(getPharmaceuticalsSubstances);
        }

        [HttpPost]
        public IActionResult GetInteractions(int pharmaceutical, int substance)
        {
            ViewData["Title"] = "Interakcje z substancjami";
            ViewData["description"] = _interactions.CheckSubstance(pharmaceutical, substance);
            var getInteractions = _interactions.GetPharmaceuticalsSubstances();
            return View(getInteractions);
        }

    }
}
