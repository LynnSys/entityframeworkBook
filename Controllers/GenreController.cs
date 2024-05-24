using BookEntityFramework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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
        public async Task<ActionResult<Genre>> CreateGenre(string genreName)
        {
            var genre = new Genre
            {
                GenreName = genreName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            _context.Genres.Add(genre);

            await _context.SaveChangesAsync();

            return Ok(genre);

        }

        [HttpDelete]
        [Route("/DeleteGenre")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var rowsAffected = _context.Genres
                .Where(g => g.GenreId == id)
                .ExecuteDeleteAsync();

            if(rowsAffected == null)
                return NotFound("Genre ID not found.");

            return Ok("Genre deleted.");
        }
    }
}
