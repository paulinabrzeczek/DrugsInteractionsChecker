using Microsoft.EntityFrameworkCore;
using InterakcjeMiedzyLekami.Models;

namespace InterakcjeMiedzyLekami.Services
{
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext _context;
        

        public BaseService(ApplicationDbContext context)
        {
            _context = context;
           
        }
    }
}
