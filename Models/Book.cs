using System;
using System.Collections.Generic;

namespace BookEntityFramework.Models;
/*Entity Framework Core, starting from version 5.0, includes native support for many-to-many relationships 
 * without the need for an explicit join table entity. Instead, it handles the join table internally. 
 * This simplifies the data model and reduces the need for boilerplate code. 
 * Here are some key points explaining why Entity Framework Core might not generate a 
 * BookAuthor model and how it handles many-to-many relationships*/

public partial class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Isbn { get; set; } = null!;
    public DateTime PublicationDate { get; set; }
    public double? Price { get; set; }
    public string? Language { get; set; }
    public string? Publisher { get; set; }
    public int? PageCount { get; set; }
    public double? AverageRating { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int GenreId { get; set; }
    public virtual Genre Genre { get; set; } = null!;
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public Book()
    {}

    public Book(BookDto bookDto)
    {
        Title = bookDto.Title;
        Description = bookDto.Description;
        Isbn = bookDto.Isbn;
        PublicationDate = bookDto.PublicationDate;
        Price = bookDto.Price;
        Language = bookDto.Language;
        Publisher = bookDto.Publisher;
        PageCount = bookDto.PageCount;
        AverageRating = bookDto.AverageRating;
        GenreId = bookDto.GenreId;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}
