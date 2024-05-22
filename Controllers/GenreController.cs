using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookEntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly LynnContext _context;

        public GenreController(LynnContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/GetAllGenres")]
        public async Task<ActionResult<List<Genre>>> GetAllGenres()
        {
            return Ok(await _context.Genres
                .Include(a => a.Books)
                .ToListAsync());
        }



        [HttpGet]
        [Route("/GetGenreById")]
        public async Task<ActionResult<Genre>> GetGenreById(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost]
        [Route("/EnterNewGenre")]
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
    }
}
