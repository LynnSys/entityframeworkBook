namespace BookEntityFramework.Models
{
    public class BookDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string Isbn { get; set; } = null!;
        public DateTime PublicationDate { get; set; }
        public double? Price { get; set; }
        public string? Language { get; set; }
        public string? Publisher { get; set; }
        public int? PageCount { get; set; }
        public double? AverageRating { get; set; }
        public int GenreId { get; set; }
        public List<int> AuthorIds { get; set; } = new List<int>();
    }
}
