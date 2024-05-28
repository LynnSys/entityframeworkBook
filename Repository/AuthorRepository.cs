using BookEntityFramework.Models;
using BookEntityFramework.Repository;
using BookEntityFramework.Services;
using Microsoft.EntityFrameworkCore;

namespace BookEntityFramework.Repository
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly LynnContext _context;

        public AuthorRepository(LynnContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorId == id);
        }

        public async Task<Author> CreateAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                Biography = authorDto.Biography,
                Birthdate = authorDto.Birthdate,
                Country = authorDto.Country,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return author;
        }

        public async Task<Author> UpdateAuthor(int id, UpdateAuthorDto updateAuthorDto)
        { 
                var updateAuthor = await _context.Authors
                .FirstOrDefaultAsync(a => a.AuthorId == id);

                if (updateAuthor == null)
                {
                    throw new Exception("Author not found, please enter valid author ID");
                }
                updateAuthor.FirstName = updateAuthorDto.FirstName;
                updateAuthor.LastName = updateAuthorDto.LastName;
                updateAuthor.Biography = updateAuthorDto.Biography;
                updateAuthor.Country = updateAuthorDto.Country;
                updateAuthor.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return updateAuthor;

        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.AuthorId == id);

            if (author == null)
                return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}