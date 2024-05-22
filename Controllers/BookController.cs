using Microsoft.AspNetCore.Mvc;

namespace BookEntityFramework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<BookController> _logger;
        private readonly LynnContext _context;
        public BookController(ILogger<BookController> logger, LynnContext context)
        {
            _logger = logger;
            _context = context;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        [Route("/GetAllBooks")]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return Ok(await _context.Books
                .Include(a=>a.Authors)
                .Include(b => b.Genre)
                .ToListAsync());
        }

        //[HttpPost]
        //[Route("/EnterNewBook")]
        //public async Task<ActionResult<List<Book>>> AddBook()
        //{
        //    return Ok(await _context.Books
        //        .Include(a => a.Authors)
        //        .Include(b => b.Genre)
        //        .ToListAsync());
        //}

        [HttpGet]
        [Route("/GetById")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        [Route("/EnterNewBook")]
        public async Task<ActionResult<Book>> CreateBook(BookDto b)
        {
            var book = new Book
            {
                Title = b.Title,
                Description = b.Description,
                Isbn = b.Isbn,
                PublicationDate = b.PublicationDate,
                Price = b.Price,
                Language = b.Language,
                Publisher = b.Publisher,
                PageCount = b.PageCount,
                AverageRating = b.AverageRating,
                GenreId = b.GenreId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };



            /*********************************************/
            var genre = await _context.Genres.FindAsync(b.GenreId);
            if (genre == null)
            {
                return BadRequest("Invalid Genre ID");
            }

            var authors = await _context.Authors
                .Where(a => b.AuthorIds.Contains(a.AuthorId))
                .ToListAsync();

            book.Authors = authors;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        [HttpPut]
        [Route("/UpdateBookByID")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto updateBookDto)
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

            var rowsAffected = await _context.Books
                .Where(b => b.BookId == id)
                .ExecuteUpdateAsync(b => b
                    .SetProperty(b => b.Description, updateBookDto.Description)
                    .SetProperty(b => b.Price, updateBookDto.Price)
                    .SetProperty(b => b.AverageRating, updateBookDto.AverageRating)
                    .SetProperty(b => b.GenreId, updateBookDto.GenreId)
                    .SetProperty(b => b.UpdatedAt, DateTime.Now)
                );

            if (rowsAffected == 0)
            {
                return NotFound("Book not found");
            }

            return Ok("Book updated successfully" );
        }

        [HttpDelete]
        [Route("/DeleteBook")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors) 
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound("Book not found");
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
