using System.Security.Claims;
using InterakcjeMiedzyLekami.Models;
using InterakcjeMiedzyLekami.ViewModels;

namespace InterakcjeMiedzyLekami.Services.Interfaces
{
    public interface IUserService
    {
        bool AddUser(User user);
        Task<ClaimsPrincipal> Login(UserVM user);
    }
}
