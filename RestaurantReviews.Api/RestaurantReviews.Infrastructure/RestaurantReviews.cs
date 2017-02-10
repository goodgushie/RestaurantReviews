namespace RestaurantReviews.Infrastructure
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RestaurantReviews : DbContext, IRestaurantReviewsContext
    {
        public RestaurantReviews()
            : base("name=RestaurantReviews")
        {
        }

        public virtual IDbSet<Restaurant> Restaurants { get; set; }
        public virtual IDbSet<Review> Reviews { get; set; }
        public virtual IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Restaurant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
