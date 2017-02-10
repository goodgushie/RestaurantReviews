using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Dtos;

namespace RestaurantReviews.Services
{
    public interface IReviewsService
    {
        IEnumerable<Review> GetReviewsByRestaurantId(int restaurantId);
        IEnumerable<Review> GetReviewsByUserId(int userId);
        int InsertReview(Review review);
        void DeleteReview(int id);
    }
}