
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using InterakcjeMiedzyLekami.Models;
using InterakcjeMiedzyLekami.Services.Interfaces;
using System;
using System.Text.RegularExpressions;
using InterakcjeMiedzyLekami.ViewModels;
using Microsoft.Extensions.Options;
using InterakcjeMiedzyLekami.Configuration.Settings;

namespace InterakcjeMiedzyLekami.Services
{
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// IOPTIONS<SETTINGS> appsetting,json
        /// </summary>
        //readonly string salt = "$2a$10$3X5I5A7K.YQZW6XolhO6ce";
        // ILogger
        private readonly ILogger<UserService> _logger;
        private readonly string _salt;
        public UserService(ApplicationDbContext context, ILogger<UserService> logger, IOptions<Settings> settings) : base(context)
        {
            _logger = logger;
            _salt = settings.Value.Salt;
        }

        public bool AddUser(User user)
        {
            try
            {
                bool usernameTaken = _context.Users.Any(u => u.Username == user.Username);

                if (!usernameTaken)
                {
                    user.Passwordhash = BCrypt.Net.BCrypt.HashPassword(user.Passwordhash,_salt);
                    user.IdRole = 2;
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Wystąpił błąd podczas dodawania użytkownika.");
                throw;
            }
        }

        public async Task<ClaimsPrincipal> Login(UserVM user)
        {
            User ?foundUser = null;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (regex.Match(user.Username).Success)
            {
                foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Username);
            }
            else
            {
                foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            }

            bool checkUserCredentials = BCrypt.Net.BCrypt.Verify(user.Passwordhash, foundUser!.Passwordhash);
            if (checkUserCredentials)
            {
                var userRole = _context.Roles.FirstOrDefault(u => u.IdRole == foundUser.IdRole)!;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, userRole.NameRole),
                    new Claim(ClaimTypes.Name, foundUser.Username),
                    new Claim(ClaimTypes.NameIdentifier, foundUser.IdUser.ToString())
                };


                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                return principal;
            }
            else
                return null;
        }
    }
}
