using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Infrastructure;
using RestaurantReviews.Translators;
using Review = RestaurantReviews.Dtos.Review;

namespace RestaurantReviews.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IRestaurantReviewsContext _context;

        public ReviewsService() : this(new Infrastructure.RestaurantReviews())
        {
            
        }

        public ReviewsService(IRestaurantReviewsContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetReviewsByRestaurantId(int restaurantId)
        {
            return _context.Reviews.Where(x => x.RestaurantId == restaurantId).AsReviewDtos();
        }

        public IEnumerable<Review> GetReviewsByUserId(int userId)
        {
            return _context.Reviews.Where(x => x.UserId == userId).AsReviewDtos();
        }

        public int InsertReview(Review review)
        {
            var newReview = review.AsReviewEntity();
            _context.Reviews.Add(newReview);
            _context.SaveChanges();
            return newReview.Id;
        }

        public void DeleteReview(int id)
        {
            var review = _context.Reviews.First(x => x.Id == id);
            review.Active = false;
            _context.SaveChanges();
        }
    }
}