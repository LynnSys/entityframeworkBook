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

    }
}
