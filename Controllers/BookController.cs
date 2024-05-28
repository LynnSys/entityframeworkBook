using BookEntityFramework.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookEntityFramework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {

        private readonly IBookRepository _bookRepository;
        private readonly LynnContext _context;
        public BookController(IBookRepository bookRepository, LynnContext context)
        {
            _bookRepository = bookRepository;
            _context = context;
        }


        [HttpGet]
        [Route("/GetAllBooks")]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var results = await _bookRepository.GetAllBooks();
            if (results == null)
                return BadRequest("No books present in database.");
            return Ok(results);
        }


        [HttpGet]
        [Route("/GetById")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var results = _bookRepository.GetBookById(id);
            if (results == null)
                return BadRequest("No books present in database.");
            return Ok(results);
        }

        [HttpPost]
        [Route("/EnterNewBook")]
        public async Task<ActionResult<Book>> CreateBook(BookDto bk)
        {
            var book = _bookRepository.CreateBook(bk);
            if (bk == null)
                return BadRequest("Book creation unsuccessful");
            return Ok(book);
        }

        [HttpPut]
        [Route("/UpdateBookByID")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            var genre = await _context.Genres.FindAsync(updateBookDto.GenreId);
            if (genre == null)
            {
                return BadRequest("Invalid Genre Id");
            }

            var authors = await _context.Authors
                .Where(a => updateBookDto.AuthorIds.Contains(a.AuthorId))
                .ToListAsync();

            if (authors.Count != updateBookDto.AuthorIds.Count)
            {
                return BadRequest("One or more AuthorIds are invalid");
            }

            return Ok(_bookRepository.UpdateBook(id, updateBookDto));

            //if (updatebook == null)
            //{
            //    return NotFound("Book not found");
            //}

            //return Ok(updatebook);
        }

        [HttpDelete]
        [Route("/DeleteBook")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var results = await _bookRepository.DeleteBook(id);
            if (results.Equals(false))
                return NotFound("Book Not Found");
            else
            return Ok("Deletion successful");
        }

    }
}
