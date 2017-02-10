using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using RestaurantReviews.Dtos;

namespace RestaurantReviews.Translators.Tests
{
    [TestClass]
    public class ReviewExtensionMethodsTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void AsReviewDtos_Called_ReturnsReviewDtos()
        {
            // Arrange
            var reviews = _fixture.Build<Infrastructure.Review>()
                    .With(x => x.User,
                        _fixture.Build<Infrastructure.User>().Without(y=>y.Reviews).Create())
                    .With(x => x.Restaurant,
                        _fixture.Build<Infrastructure.Restaurant>().Without(y => y.Reviews).Create())
                    .CreateMany()
                    .ToList();

            // Act
            var results = reviews.AsReviewDtos();

            // Assert
            Assert.AreEqual(reviews.Count, results.Count());
        }

        [TestMethod]
        public void AsReviewDto_Called_ReturnsTranslatedReviewDto()
        {
            // Arrange
            var review = _fixture.Build<Infrastructure.Review>()
                .With(x => x.User,
                    _fixture.Build<Infrastructure.User>().Without(y => y.Reviews).Create())
                .With(x => x.Restaurant,
                    _fixture.Build<Infrastructure.Restaurant>().Without(y => y.Reviews).Create())
                .Create();

            // Act
            var result = review.AsReviewDto();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Review));
            Assert.AreEqual(review.Id, result.Id);
            Assert.AreEqual(review.Description, result.Description);
            Assert.AreEqual(review.Rating, result.Rating);
            Assert.AreEqual(review.UserId, result.UserId);
            Assert.AreEqual(review.User.ScreenName, result.UserName);
            Assert.AreEqual(review.RestaurantId, result.RestaurantId);
            Assert.AreEqual(review.Restaurant.Name, result.RestaurantName);
        }

        [TestMethod]
        public void AsReviewEntities_Called_ReturnsReviewEntities()
        {
            // Arrange
            var reviews = _fixture.CreateMany<Review>().ToList();

            // Act
            var results = reviews.AsReviewEntities();

            // Assert
            Assert.AreEqual(reviews.Count, results.Count());
        }

        [TestMethod]
        public void AsReviewEntity_Called_ReturnsTranslatedReviewEntities()
        {
            // Arrange
            var review = _fixture.Create<Review>();

            // Act
            var result = review.AsReviewEntity();

            // Assert
            Assert.IsInstanceOfType(result, typeof(Infrastructure.Review));
            Assert.AreEqual(review.Id, result.Id);
            Assert.AreEqual(review.Description, result.Description);
            Assert.AreEqual(review.Rating, result.Rating);
            Assert.AreEqual(review.UserId, result.UserId);
            Assert.AreEqual(review.RestaurantId, result.RestaurantId);
        }
    }
}