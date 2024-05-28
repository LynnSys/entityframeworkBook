using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookEntityFramework.Services
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAllBooks();
        public Task<Book> GetBookById(int id);
        public Task<Book> CreateBook(BookDto bookDto);
        public string UpdateBook(int id, UpdateBookDto updateBookDto);
        public Task<bool> DeleteBook(int id);
    }
}
