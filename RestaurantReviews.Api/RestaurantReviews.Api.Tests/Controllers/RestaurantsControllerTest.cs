using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using RestaurantReviews.Api.Controllers;
using RestaurantReviews.Dtos;
using RestaurantReviews.Services;

namespace RestaurantReviews.Api.Tests.Controllers
{
    [TestClass]
    public class RestaurantsControllerTest
    {
        private Fixture _fixture;
        private IRestaurantsService _service;
        private RestaurantsController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _service = A.Fake<IRestaurantsService>();
            _controller = new RestaurantsController(_service);
        }

        [TestMethod]
        public void GetRestaurantsList_Called_ReturnsRestaurants()
        {
            // Arrange
            var restaurants = _fixture.CreateMany<Restaurant>().ToList();
            A.CallTo(() => _service.GetAllRestaurants()).Returns(restaurants);

            // Act
            var results = _controller.GetRestaurantsList();

            // Assert
            Assert.AreEqual(restaurants.Count, results.Count());
        }

        [TestMethod]
        public void GetRestaurant_Called_ReturnsRestaurant()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            A.CallTo(() => _service.GetRestaurant(restaurant.Id)).Returns(restaurant);

            // Act
            var result = _controller.GetRestaurant(restaurant.Id);

            // Assert
            Assert.AreEqual(restaurant.Id, result.Id);
        }

        [TestMethod]
        public void FindRestaurantsByCity_Called_ReturnsRestaurants()
        {
            // Arrange
            var city = _fixture.Create<string>();
            var restaurants = _fixture.CreateMany<Restaurant>().ToList();
            A.CallTo(() => _service.SearchByCity(city)).Returns(restaurants);

            // Act
            var results = _controller.FindRestaurantsByCity(city);

            // Assert
            Assert.AreEqual(restaurants.Count, results.Count());
        }

        [TestMethod]
        public void InsertReview_Called_ReturnsNewId()
        {
            // Arrange
            var restaurant = _fixture.Create<Restaurant>();
            var restaurantId = _fixture.Create<int>();
            A.CallTo(() => _service.InsertRestaurant(restaurant)).Returns(restaurantId);

            // Act
            var result = _controller.InsertRestaurant(restaurant);

            // Assert
            Assert.AreEqual(restaurantId, result);
        }
    }
}
