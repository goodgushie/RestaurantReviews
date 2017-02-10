namespace RestaurantReviews.Dtos
{
    public class Review
    {
        public int Id;
        public int UserId;
        public string UserName;
        public int RestaurantId;
        public string RestaurantName;
        public int Rating;
        public string Description;
    }
}