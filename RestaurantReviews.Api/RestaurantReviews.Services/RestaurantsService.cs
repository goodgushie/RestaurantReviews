using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Infrastructure;
using RestaurantReviews.Translators;
using Restaurant = RestaurantReviews.Dtos.Restaurant;

namespace RestaurantReviews.Services
{
    public class RestaurantsService : IRestaurantsService
    {
        private readonly IRestaurantReviewsContext _context;

        public RestaurantsService() : this(new Infrastructure.RestaurantReviews())
        {
            
        }

        public RestaurantsService(IRestaurantReviewsContext context)
        {
            _context = context;
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.AsRestaurantDtos();
        }

        public Restaurant GetRestaurant(int id)
        {
            return _context.Restaurants.FirstOrDefault(x => x.Id == id).AsRestaurantDto();
        }

        public IEnumerable<Restaurant> SearchByCity(string term)
        {
            return _context.Restaurants.Where(x => x.City == term).AsRestaurantDtos();
        }

        public int InsertRestaurant(Restaurant restaurant)
        {
            var newRestaurant = restaurant.AsRestaurantEntity();
            _context.Restaurants.Add(newRestaurant);
            _context.SaveChanges();
            return newRestaurant.Id;
        }
    }
}