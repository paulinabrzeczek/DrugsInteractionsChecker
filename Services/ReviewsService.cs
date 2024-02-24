using InterakcjeMiedzyLekami.Models;
using InterakcjeMiedzyLekami.Services.Interfaces;
using InterakcjeMiedzyLekami.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterakcjeMiedzyLekami.Services
{
    public class ReviewsService : BaseService, IReviews
    {
        
        public ReviewsService(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task AddReview(PharmaceuticalsReview review)
        {
            _context.PharmaceuticalsReviews.Add(review);
             await _context.SaveChangesAsync();
        }

        public async Task<List<UserReviewsVM>> GetReviews(int pharmaceuticalId)
        {
            List<UserReviewsVM> userReviews = new List<UserReviewsVM>();
            var getReviewsFromDB = await _context.PharmaceuticalsReviews
                .Where(r => r.IdPharmaceutical == pharmaceuticalId)
                .ToListAsync();
            foreach(var userReview in getReviewsFromDB)
            {
                string UName;
                User zm = _context.Users.Where(x => x.IdUser == userReview.IdUser).FirstOrDefault()!;
                if(zm != null)
                {
                    UName = zm.Username;
                }
                else 
                {
                    UName = "Brak";
                }
                UserReviewsVM temp = new UserReviewsVM
                {
                    CreationDate = userReview.CreationDate,
                    Review = userReview.Review,
                    UserName = UName
                };

                userReviews.Add(temp);
            }
            return userReviews;
        }
    }
}
