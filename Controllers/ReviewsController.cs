using InterakcjeMiedzyLekami.DTO;
using InterakcjeMiedzyLekami.Models;
using InterakcjeMiedzyLekami.Services;
using InterakcjeMiedzyLekami.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InterakcjeMiedzyLekami.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IReviews _reviews;

        public ReviewsController( ApplicationDbContext context, IReviews reviews)
        {
            _context = context;
            _reviews = reviews;
        }

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> AddReview()
        {
            ViewData["Title"] = "Dodaj opinie";
            return View("AddReview");
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> AddReview(int id, string reviewText)
        {
            ViewData["Title"] = "Dodaj opinie";
            var NameId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var review = new PharmaceuticalsReview
            {
                IdPharmaceutical = id,
                IdUser = NameId,
                Review = reviewText,
                CreationDate = DateTime.Now
            };
            _context.PharmaceuticalsReviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddReview", new { id = id });
        }

        public IActionResult GetReviews()
        {
            ViewData["Title"] = "Opinie";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(int id)
        {
            ViewData["Title"] = "Opinie";
            var reviews = await _reviews.GetReviews(id);
            return View(reviews);
        }

    }
}
