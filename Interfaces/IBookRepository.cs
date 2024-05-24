namespace BookEntityFramework.Services
{
    public interface IBookRepository
    {
        public List<Book> GetAllBooks();
        public Book AddBook(BookDto book, List<int> authors);
        public Book AddBookWithAuthors(BookDto book, List<int> authors);
        public Book GetById(int id);
        public Book UpdateBook(int id, UpdateBookDto book);
        public List<Book> DeleteBookById(int id);
    }
}
