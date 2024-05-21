namespace BookEntityFramework.Models
{
    public class UpdateBookDto
    {
        public string Description { get; set; }
        public float Price { get; set; }
        public float AverageRating { get; set; }
        public int GenreId { get; set; }
        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
