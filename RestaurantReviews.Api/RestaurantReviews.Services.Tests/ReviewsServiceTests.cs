using System.Data.Entity;
using System.Linq;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using RestaurantReviews.Infrastructure;

namespace RestaurantReviews.Services.Tests
{
    [TestClass]
    public class ReviewsServiceTests
    {
        private Fixture _fixture;
        private IRestaurantReviewsContext _context;
        private IDbSet<Review> _reviewsSet;
        private IReviewsService _service;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _reviewsSet = new FakeDbSet<Review>();
            _context = A.Fake<IRestaurantReviewsContext>();
            _context.Reviews = _reviewsSet;
            _service = new ReviewsService(_context);
        }

        [TestMethod]
        public void GetReviewsByRestaurantId_ExistingId_ReturnsReviews()
        {
            // Arrange
            var restaurantId = _fixture.Create<int>();
            var reviewsWithId = _fixture.Build<Review>()
                    .With(x => x.RestaurantId, restaurantId)
                    .With(x => x.User,
                        _fixture.Build<User>().Without(y => y.Reviews).Create())
                    .With(x => x.Restaurant,
                        _fixture.Build<Restaurant>().Without(y => y.Reviews).Create())
                    .CreateMany().ToList();
            reviewsWithId.ForEach(x => _reviewsSet.Add(x));

            var otherId = _fixture.Create<int>();
            var reviewsWithoutId = _fixture.Build<Review>()
                    .With(x => x.RestaurantId, otherId)
                    .Without(x => x.User)
                    .Without(x => x.Restaurant)
                    .CreateMany().ToList();
            reviewsWithoutId.ForEach(x => _reviewsSet.Add(x));

            // Act
            var results = _service.GetReviewsByRestaurantId(restaurantId);

            // Assert
            Assert.AreEqual(reviewsWithId.Count, results.Count());
        }

        [TestMethod]
        public void GetReviewsByRestaurantId_NotExistingId_ReturnsReviews()
        {
            // Arrange
            var restaurantId = _fixture.Create<int>();
            var otherId = _fixture.Create<int>();
            var reviewsWithoutId = _fixture.Build<Review>()
                    .With(x => x.RestaurantId, otherId)
                    .Without(x => x.User)
                    .Without(x => x.Restaurant)
                    .CreateMany().ToList();
            reviewsWithoutId.ForEach(x => _reviewsSet.Add(x));

            // Act
            var results = _service.GetReviewsByRestaurantId(restaurantId);

            // Assert
            Assert.AreEqual(0, results.Count());
        }

        [TestMethod]
        public void GetReviewsByUserId_ExistingId_ReturnsReviews()
        {
            // Arrange
            var userId = _fixture.Create<int>();
            var reviewsWithId = _fixture.Build<Review>()
                    .With(x => x.UserId, userId)
                    .With(x => x.User,
                        _fixture.Build<User>().Without(y => y.Reviews).Create())
                    .With(x => x.Restaurant,
                        _fixture.Build<Restaurant>().Without(y => y.Reviews).Create())
                    .CreateMany().ToList();
            reviewsWithId.ForEach(x => _reviewsSet.Add(x));

            var otherId = _fixture.Create<int>();
            var reviewsWithoutId = _fixture.Build<Review>()
                    .With(x => x.RestaurantId, otherId)
                    .Without(x => x.User)
                    .Without(x => x.Restaurant)
                    .CreateMany().ToList();
            reviewsWithoutId.ForEach(x => _reviewsSet.Add(x));

            // Act
            var results = _service.GetReviewsByUserId(userId);

            // Assert
            Assert.AreEqual(reviewsWithId.Count, results.Count());
        }

        [TestMethod]
        public void GetReviewsByUserId_NotExistingId_ReturnsReviews()
        {
            // Arrange
            var userId = _fixture.Create<int>();
            var otherId = _fixture.Create<int>();
            var reviewsWithoutId = _fixture.Build<Review>()
                    .With(x => x.UserId, otherId)
                    .Without(x => x.User)
                    .Without(x => x.Restaurant)
                    .CreateMany().ToList();
            reviewsWithoutId.ForEach(x => _reviewsSet.Add(x));

            // Act
            var results = _service.GetReviewsByUserId(userId);

            // Assert
            Assert.AreEqual(0, results.Count());
        }

        [TestMethod]
        public void InsertReview_NewReview_ReviewInserted()
        {
            // Arrange
            var review = _fixture.Create<Dtos.Review>();

            // Act
            _service.InsertReview(review);

            // Assert
            A.CallTo(() => _context.SaveChanges()).MustHaveHappened(Repeated.Exactly.Once);
            Assert.AreEqual(1, _reviewsSet.Count());
        }

        [TestMethod]
        public void DeleteReview_Id_SetsReviewAsInactive()
        {
            // Arrange
            var reviews = _fixture.Build<Review>()
                    .With(x => x.Active, true)
                    .Without(x => x.User)
                    .Without(x => x.Restaurant)
                    .CreateMany().ToList();
            reviews.ForEach(x => _reviewsSet.Add(x));

            // Act
            _service.DeleteReview(reviews[0].Id);

            // Assert
            A.CallTo(() => _context.SaveChanges()).MustHaveHappened(Repeated.Exactly.Once);
            Assert.IsFalse(reviews[0].Active);
        }
    }
}