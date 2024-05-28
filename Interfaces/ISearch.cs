namespace BookEntityFramework.Interfaces
{
    public interface ISearch
    {
        public Task<List<Book>> GetBooksByAuthor(int authorId);
        public Task<Book> GetBookByTitle(string title);
        public List<Book> GetBooksByGenre(int genreId);

    }
}
