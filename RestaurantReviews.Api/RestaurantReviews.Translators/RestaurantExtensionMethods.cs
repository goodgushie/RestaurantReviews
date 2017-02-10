using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Dtos;

namespace RestaurantReviews.Translators
{
    public static class RestaurantExtensionMethods
    {
        public static IEnumerable<Restaurant> AsRestaurantDtos(this IEnumerable<Infrastructure.Restaurant> values)
        {
            return values.Select(x => x.AsRestaurantDto());
        }

        public static Restaurant AsRestaurantDto(this Infrastructure.Restaurant value)
        {
            return value == null
                ? null
                : new Restaurant
                {
                    Id = value.Id,
                    AddressLine1 = value.AddressLine1,
                    AddressLine2 = value.AddressLine2,
                    Name = value.Name,
                    City = value.City,
                    Country = value.Country,
                    PostalCode = value.PostalCode,
                    Province = value.Province
                };
        }

        public static IEnumerable<Infrastructure.Restaurant> AsRestaurantEntities(this IEnumerable<Restaurant> values)
        {
            return values.Select(x => x.AsRestaurantEntity());
        }

        public static Infrastructure.Restaurant AsRestaurantEntity(this Restaurant value)
        {
            return value == null
                ? null
                : new Infrastructure.Restaurant
                {
                    Id = value.Id,
                    AddressLine1 = value.AddressLine1,
                    AddressLine2 = value.AddressLine2,
                    Name = value.Name,
                    City = value.City,
                    Country = value.Country,
                    PostalCode = value.PostalCode,
                    Province = value.Province
                };
        }
    }
}