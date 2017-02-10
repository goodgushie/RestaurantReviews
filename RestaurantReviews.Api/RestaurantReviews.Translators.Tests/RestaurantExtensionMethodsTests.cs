using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using RestaurantReviews.Dtos;

namespace RestaurantReviews.Translators.Tests
{
    [TestClass]
    public class RestaurantExtensionMethodsTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void AsRestaurantsDtos_Called_ReturnsRestaurantDtos()
        {
            // Arrange
            var restaurants = _fixture.Build<Infrastructure.Restaurant>()
                .Without(x => x.Reviews).CreateMany().ToList();

            // Act
            var results = restaurants.AsRestaurantDtos();

            // Assert
            Assert.AreEqual(restaurants.Count, results.Count());
        }

        [TestMethod]
        public void AsRestaurantDto_Called_ReturnsTranslatedRestaurantDto()
        {
            // Arrange
            var restaurant = _fixture.Build<Infrastructure.Restaurant>()
                .Without(x => x.Reviews).Create();

            // Act
            var result = restaurant.AsRestaurantDto();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Restaurant));
            Assert.AreEqual(restaurant.Id, result.Id);
            Assert.AreEqual(restaurant.Name, result.Name);
            Assert.AreEqual(restaurant.AddressLine1, result.AddressLine1);
            Assert.AreEqual(restaurant.AddressLine2, result.AddressLine2);
            Assert.AreEqual(restaurant.Country, result.Country);
            Assert.AreEqual(restaurant.City, result.City);
            Assert.AreEqual(restaurant.PostalCode, result.PostalCode);
            Assert.AreEqual(restaurant.Province, result.Province);
        }

        [TestMethod]
        public void AsRestaurantEntities_Called_ReturnsRestaurantEntities()
        {
            // Arrange
            var restaurants = _fixture.CreateMany<Restaurant>().ToList();

            // Act
            var results = restaurants.AsRestaurantEntities();

            // Assert
            Assert.AreEqual(restaurants.Count, results.Count());
        }

        [TestMethod]
        public void AsRestaurantEntity_Called_ReturnsTranslatedRestaurantEntities()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();

            // Act
            var result = restaurant.AsRestaurantEntity();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Infrastructure.Restaurant));
            Assert.AreEqual(restaurant.Id, result.Id);
            Assert.AreEqual(restaurant.Name, result.Name);
            Assert.AreEqual(restaurant.AddressLine1, result.AddressLine1);
            Assert.AreEqual(restaurant.AddressLine2, result.AddressLine2);
            Assert.AreEqual(restaurant.Country, result.Country);
            Assert.AreEqual(restaurant.City, result.City);
            Assert.AreEqual(restaurant.PostalCode, result.PostalCode);
            Assert.AreEqual(restaurant.Province, result.Province);
        }
    }
}
