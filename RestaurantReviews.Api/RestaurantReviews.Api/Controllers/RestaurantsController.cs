using System.Collections.Generic;
using System.Web.Http;
using RestaurantReviews.Dtos;
using RestaurantReviews.Services;

namespace RestaurantReviews.Api.Controllers
{
    [RoutePrefix("v1")]
    public class RestaurantsController : ApiController
    {
        private readonly IRestaurantsService _service;

        public RestaurantsController(IRestaurantsService service)
        {
            _service = service;
        }

        public RestaurantsController() : this (new RestaurantsService())
        {
        }

        [HttpGet]
        [Route("restaurants")]
        public IEnumerable<Restaurant> GetRestaurantsList()
        {
            return _service.GetAllRestaurants();
        }

        [HttpGet]
        [Route("restaurants/{id:int}")]
        public Restaurant GetRestaurant(int id)
        {
            return _service.GetRestaurant(id);
        }

        [HttpGet]
        [Route("restaurants/{term:alpha}")]
        public IEnumerable<Restaurant> FindRestaurantsByCity(string term)
        {
            return _service.SearchByCity(term);
        }

        [HttpPost]
        [Route("restaurants")]
        public int InsertRestaurant(Restaurant restaurant)
        {
            return _service.InsertRestaurant(restaurant);
        }
    }
}