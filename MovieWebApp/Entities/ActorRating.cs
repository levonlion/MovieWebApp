namespace MovieWebApp.Entities
{
    public class ActorRating
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public string UserId { get; set; }
        public Actor Actor { get; set; }
        public ApplicationUser User { get; set; }
    }
}
