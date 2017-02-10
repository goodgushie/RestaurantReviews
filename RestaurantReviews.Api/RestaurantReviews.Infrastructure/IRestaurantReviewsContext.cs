using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviews.Infrastructure
{
    public interface IRestaurantReviewsContext
    {
        IDbSet<Restaurant> Restaurants { get; set; }
        IDbSet<Review> Reviews { get; set; }
        IDbSet<User> Users { get; set; }
        int SaveChanges();
    }
}