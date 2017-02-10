using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Dtos;

namespace RestaurantReviews.Services
{
    public interface IRestaurantsService
    {
        IEnumerable<Restaurant> GetAllRestaurants();
        Restaurant GetRestaurant(int id);
        IEnumerable<Restaurant> SearchByCity(string term);
        int InsertRestaurant(Restaurant restaurant);
    }
}