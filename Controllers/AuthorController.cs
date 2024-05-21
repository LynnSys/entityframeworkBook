using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookEntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly LynnContext _context;

        public AuthorController(LynnContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/GetAllAuthors")]
        public async Task<ActionResult<List<Author>>> GetAllAuthors()
        {
            return Ok(await _context.Authors
                .Include(a => a.Books)
                .ToListAsync());
        }

        [HttpGet]
        [Route("/GetAuthorById")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        [Route("/EnterNewAuthor")]
        public async Task<ActionResult<Author>> CreateAuthor(AuthorDto a)
        {
            var author = new Author
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                Biography = a.Biography,
                Birthdate = a.Birthdate,
                Country = a.Country,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            _context.Authors.Add(author);

    
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        [HttpPut]
        [Route("/UpdateAuthorByID")]
        public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDto updateAuthorDto)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorId == id);

            if (author == null)
            {
                return NotFound("Author not found");
            }
            author.FirstName = updateAuthorDto.FirstName;
            author.LastName = updateAuthorDto.LastName;
            author.Biography = updateAuthorDto.Biography;
            author.Country = updateAuthorDto.Country;
            author.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(author);
        }

        [HttpDelete]
        [Route("/DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorId == id);

            if (author == null)
            {
                return NotFound("Author not found");
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
