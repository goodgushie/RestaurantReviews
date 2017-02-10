namespace RestaurantReviews.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RestaurantId { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastUpdated { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public virtual User User { get; set; }
    }
}
