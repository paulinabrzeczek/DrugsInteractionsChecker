using InterakcjeMiedzyLekami.Models;
using InterakcjeMiedzyLekami.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InterakcjeMiedzyLekami.Services.Interfaces
{
    public interface IReviews
    {
        public Task AddReview (PharmaceuticalsReview review);
        public Task<List<UserReviewsVM>> GetReviews(int pharmaceuticalId);


    }
}
