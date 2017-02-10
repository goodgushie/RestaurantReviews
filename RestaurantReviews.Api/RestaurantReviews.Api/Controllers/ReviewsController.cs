using System.Collections.Generic;
using System.Web.Http;
using RestaurantReviews.Dtos;
using RestaurantReviews.Services;

namespace RestaurantReviews.Api.Controllers
{
    [RoutePrefix("v1")]
    public class ReviewsController : ApiController
    {
        private readonly IReviewsService _service;

        public ReviewsController(IReviewsService service)
        {
            _service = service;
        }

        public ReviewsController() : this(new ReviewsService())
        {
            
        }

        [HttpGet]
        [Route("reviews/restaurants/{id:int}")]
        public IEnumerable<Review> GetReviewsByRestaurantId(int id)
        {
            return _service.GetReviewsByRestaurantId(id);
        }

        [HttpGet]
        [Route("reviews/users/{id:int}")]
        public IEnumerable<Review> GetReviewsByUserId(int id)
        {
            return _service.GetReviewsByUserId(id);
        }

        [HttpPost]
        [Route("reviews")]
        public int InsertReview(Review review)
        {
            return _service.InsertReview(review);
        }

        [HttpDelete]
        [Route("reviews/{id:int}")]
        public void DeleteReview(int id)
        {
            _service.DeleteReview(id);
        }
    }
}