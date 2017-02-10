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
    public class ReviewsControllerTest
    {
        private Fixture _fixture;
        private IReviewsService _service;
        private ReviewsController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _service = A.Fake<IReviewsService>();
            _controller = new ReviewsController(_service);
        }

        [TestMethod]
        public void GetReviewsByRestaurantId_Called_ReturnsReviews()
        {
            // Arrange
            var reviews = _fixture.CreateMany<Review>().ToList();
            var restaurantId = _fixture.Create<int>();
            A.CallTo(() => _service.GetReviewsByRestaurantId(restaurantId)).Returns(reviews);

            // Act
            var results = _controller.GetReviewsByRestaurantId(restaurantId);

            // Assert
            Assert.AreEqual(reviews.Count, results.Count());
        }

        [TestMethod]
        public void GetReviewsByUserId_Called_ReturnsReviews()
        {
            // Arrange
            var reviews = _fixture.CreateMany<Review>().ToList();
            var userId = _fixture.Create<int>();
            A.CallTo(() => _service.GetReviewsByUserId(userId)).Returns(reviews);

            // Act
            var results = _controller.GetReviewsByUserId(userId);

            // Assert
            Assert.AreEqual(reviews.Count, results.Count());
        }

        [TestMethod]
        public void InsertReview_Called_ReturnsNewId()
        {
            // Arrange
            var review = _fixture.Create<Review>();
            var reviewId = _fixture.Create<int>();
            A.CallTo(() => _service.InsertReview(review)).Returns(reviewId);

            // Act
            var result = _controller.InsertReview(review);

            // Assert
            Assert.AreEqual(reviewId, result);
        }

        [TestMethod]
        public void DeleteReview_Called_CallsService()
        {
            // Arrange
            var reviewId = _fixture.Create<int>();

            // Act
            _controller.DeleteReview(reviewId);

            // Assert
            A.CallTo(() => _service.DeleteReview(reviewId)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
