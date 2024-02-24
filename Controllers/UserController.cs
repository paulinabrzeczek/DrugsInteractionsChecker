using InterakcjeMiedzyLekami.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InterakcjeMiedzyLekami.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using InterakcjeMiedzyLekami.ViewModels;

namespace InterakcjeMiedzyLekami.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IConfiguration configuration, ApplicationDbContext context, IUserService userService)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
            _userService = userService;
        }

        //[Authorize(Policy = "RequireLoggedIn")]
        

        [HttpPost]
        public IActionResult AddUser([FromForm] User user)
        {
            ViewData["Title"] = "Rejestracja";
            var isUserAdded = _userService.AddUser(user);

            if (isUserAdded)
            {
                ViewBag.UserAdded = true;
                return Json(new { redirectToUrl = Url.Action("Index") });
            }

            return Json(new { message = "Username is already taken" });
        }


        [HttpPost]
        public async Task<IActionResult> LoginUser([FromForm] UserVM user)
        {
            ViewData["Title"] = "Logowanie";
            Task<ClaimsPrincipal> principal = _userService.Login(user);

            if(principal.Result != null)
            {
                await HttpContext.SignInAsync(principal.Result);

                return RedirectToAction("Index", "Home");
            }

            return Json(new { message = "Nazwa użytkownika lub hasło nieprawidłowe" });
        }

        public async Task<IActionResult> LogoutUser()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            ViewData["Title"] = "Logowanie";
            return View();
        }

        public IActionResult Register()
        {
            ViewData["Title"] = "Rejestracja";
            return View();
        }
    }
}
