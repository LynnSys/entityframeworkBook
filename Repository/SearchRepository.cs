using BookEntityFramework.Interfaces;

namespace BookEntityFramework.Repository
{
    public class SearchRepository : ISearch
    {
        private readonly LynnContext _context;
        public SearchRepository(LynnContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksByAuthor(int authorId)
        {
            var books = await _context.Books
                .Where(b => b.Authors.Any(a => a.AuthorId == authorId))
                .Include(b => b.Authors)
                .ToListAsync();
            return books;
        }
        public async Task<Book> GetBookByTitle(string title)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Title == title);
            return book;
        }

        public List<Book> GetBooksByGenre(int genreId)
        {
            return _context.Books
                .Where(b => b.GenreId == genreId)
                .Include(b => b.Authors)
                .ToList();
        }
    }
}
