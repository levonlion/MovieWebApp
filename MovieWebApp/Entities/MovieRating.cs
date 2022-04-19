namespace MovieWebApp.Entities
{
    public class MovieRating
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public Movie Movie { get; set; }
        public ApplicationUser User { get; set; }
    }
}
