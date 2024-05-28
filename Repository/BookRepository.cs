using BookEntityFramework.Models;
using BookEntityFramework.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookEntityFramework.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LynnContext _context;

        public BookRepository(LynnContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books
                .Include(b => b.Authors)
                .ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<Book> CreateBook(BookDto bookDto)
        {
            var genre = await _context.Genres.FindAsync(bookDto.GenreId);
            if (genre == null)
            {
                throw new ArgumentException("Invalid Genre ID");
            }

            var authors = await _context.Authors
                .Where(a => bookDto.AuthorIds.Contains(a.AuthorId))
                .ToListAsync();

            if (authors.Count != bookDto.AuthorIds.Count)
            {
                throw new ArgumentException("One or more AuthorIds are invalid");
            }

            var book = new Book(bookDto);
            book.Authors = authors;
            //var book = new Book
            //{
            //    Title = bookDto.Title,
            //    Description = bookDto.Description,
            //    Isbn = bookDto.Isbn,
            //    PublicationDate = bookDto.PublicationDate,
            //    Price = bookDto.Price,
            //    Language = bookDto.Language,
            //    Publisher = bookDto.Publisher,
            //    PageCount = bookDto.PageCount,
            //    AverageRating = bookDto.AverageRating,
            //    GenreId = bookDto.GenreId,
            //    CreatedAt = DateTime.Now,
            //    UpdatedAt = DateTime.Now,
            //    Authors = authors
            //};

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public string UpdateBook(int bookID, UpdateBookDto book)
        {
            try
            {
                var updateBook = _context.Books.Include(b => b.Authors).FirstOrDefault(b => b.BookId == bookID);

                if (updateBook == null)
                {
                    throw new Exception("Book not found, please enter valid book ID");
                }

                updateBook.Description = book.Description;
                updateBook.Price = book.Price;
                updateBook.AverageRating = book.AverageRating;
                updateBook.GenreId = book.GenreId;
                updateBook.UpdatedAt = DateTime.Now;
                updateBook.Authors.Clear();

                var existingAuthors = _context.Authors.Where(a => book.AuthorIds.Contains(a.AuthorId)).ToList();

                foreach (var author in existingAuthors)
                {
                    updateBook.Authors.Add(author);
                }

                _context.SaveChanges();
                return "Book updated successfully";
            }

            catch (Exception bookNotFound)
            {
                return bookNotFound.Message;
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        

        

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return false;
            }

            book.Authors.Clear();
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }   
    }
}
