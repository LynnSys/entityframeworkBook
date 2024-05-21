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
            // Map DTO to Entity
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

            // Assign authors to the book
            book.Authors = authors;

            // Add the book to the context
            _context.Books.Add(book);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        [HttpPut("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            // Find the existing book by ID
            var book = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return NotFound("Book not found");
            }

            // Fetch the genre from the database
            var genre = await _context.Genres.FindAsync(updateBookDto.GenreId);
            if (genre == null)
            {
                return BadRequest("Invalid GenreId");
            }

            // Fetch authors from the database
            var authors = await _context.Authors
                .Where(a => updateBookDto.AuthorIds.Contains(a.AuthorId))
                .ToListAsync();

            if (authors.Count != updateBookDto.AuthorIds.Count)
            {
                return BadRequest("One or more AuthorIds are invalid");
            }

            // Update the book properties
            book.Description = updateBookDto.Description;
            book.Price = updateBookDto.Price;
            book.AverageRating = updateBookDto.AverageRating;
            book.GenreId = updateBookDto.GenreId;
            book.Genre = genre;
            book.Authors = authors;
            book.UpdatedAt = DateTime.Now;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the updated book
            return Ok(book);
        }

    }
}
