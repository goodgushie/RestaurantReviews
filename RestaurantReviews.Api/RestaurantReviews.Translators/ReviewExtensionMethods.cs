using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Dtos;

namespace RestaurantReviews.Translators
{
    public static class ReviewExtensionMethods
    {
        public static IEnumerable<Review> AsReviewDtos(this IEnumerable<Infrastructure.Review> values)
        {
            return values.Select(x => x.AsReviewDto());
        }

        public static Review AsReviewDto(this Infrastructure.Review value)
        {
            return value == null
                ? null
                : new Review
                {
                    Description = value.Description,
                    Id = value.Id,
                    Rating = value.Rating,
                    RestaurantId = value.RestaurantId,
                    RestaurantName = value.Restaurant.Name,
                    UserId = value.UserId,
                    UserName = value.User.ScreenName
                };
        }

        public static IEnumerable<Infrastructure.Review> AsReviewEntities(this IEnumerable<Review> values)
        {
            return values.Select(x => x.AsReviewEntity());
        }

        public static Infrastructure.Review AsReviewEntity(this Review value)
        {
            return value == null
                ? null
                : new Infrastructure.Review
                {
                    Description = value.Description,
                    Id = value.Id,
                    Rating = value.Rating,
                    RestaurantId = value.RestaurantId,
                    UserId = value.UserId
                };
        }
    }
}