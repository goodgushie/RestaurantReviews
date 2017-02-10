using System.Data.Entity;
using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using RestaurantReviews.Infrastructure;

namespace RestaurantReviews.Services.Tests
{
    [TestClass]
    public class RestaurantsServiceTests
    {
        private Fixture _fixture;
        private IRestaurantReviewsContext _context;
        private IDbSet<Restaurant> _restaurantsSet;
        private IRestaurantsService _service;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _restaurantsSet = new FakeDbSet<Restaurant>();
            _context = A.Fake<IRestaurantReviewsContext>();
            _context.Restaurants = _restaurantsSet;
            _service = new RestaurantsService(_context);
        }

        [TestMethod]
        public void GetAllRestaurants_NoParams_ReturnsRestaurants()
        {
            // Arrange
            var restaurants = _fixture.Build<Restaurant>()
                .Without(x => x.Reviews).CreateMany().ToList();
            restaurants.ForEach(x => _restaurantsSet.Add(x));

            // Act
            var results = _service.GetAllRestaurants();

            // Assert
            Assert.AreEqual(restaurants.Count, results.Count());
        }

        [TestMethod]
        public void GetRestaurant_KnownId_ReturnsRestaurant()
        {
            // Arrange
            var restaurants = _fixture.Build<Restaurant>()
                .Without(x => x.Reviews).CreateMany().ToList();
            restaurants.ForEach(x => _restaurantsSet.Add(x));

            // Act
            var result = _service.GetRestaurant(restaurants[0].Id);

            // Assert
            Assert.AreEqual(restaurants[0].Name, result.Name);
        }

        [TestMethod]
        public void GetRestaurant_UnknownId_ReturnsNull()
        {
            // Arrange
            var staticId = _fixture.Create<int>();
            var restaurants = _fixture.Build<Restaurant>()
                .Without(x => x.Reviews).CreateMany().ToList();
            restaurants.ForEach(x => _restaurantsSet.Add(x));

            // Act
            var result = _service.GetRestaurant(staticId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SearchByCity_ExistingCity_ReturnsRestaurants()
        {
            // Arrange
            var searchCity = _fixture.Create<string>();
            var restaurants = _fixture.Build<Restaurant>()
                .With(x => x.City, searchCity)
                .Without(x => x.Reviews).CreateMany().ToList();
            restaurants.ForEach(x => _restaurantsSet.Add(x));

            var otherCity = _fixture.Create<string>();
            var otherRestaurants = _fixture.Build<Restaurant>()
                .With(x => x.City, otherCity)
                .Without(x => x.Reviews).CreateMany().ToList();
            otherRestaurants.ForEach(x => _restaurantsSet.Add(x));

            // Act
            var results = _service.SearchByCity(searchCity);

            // Assert
            Assert.AreEqual(restaurants.Count, results.Count());
        }

        [TestMethod]
        public void SearchByCity_NoExistingCity_ReturnsRestaurants()
        {
            // Arrange
            var searchCity = _fixture.Create<string>();
            var otherCity = _fixture.Create<string>();
            var otherRestaurants = _fixture.Build<Restaurant>()
                .With(x => x.City, otherCity)
                .Without(x => x.Reviews).CreateMany().ToList();
            otherRestaurants.ForEach(x => _restaurantsSet.Add(x));

            // Act
            var results = _service.SearchByCity(searchCity);

            // Assert
            Assert.AreEqual(0, results.Count());
        }

        [TestMethod]
        public void InsertReview_NewReview_ReviewInserted()
        {
            // Arrange
            var restaurant = _fixture.Create<Dtos.Restaurant>();

            // Act
            _service.InsertRestaurant(restaurant);

            // Assert
            A.CallTo(() => _context.SaveChanges()).MustHaveHappened(Repeated.Exactly.Once);
            Assert.AreEqual(1, _restaurantsSet.Count());
        }
    }
}
