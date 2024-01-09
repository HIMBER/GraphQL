using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PocGraphQL.Api.Model;

[Table("book")]
public class Book
{
    public Book(int id, string title, int authorId)
    {
        Id = id;
        Title = title;
        AuthorId = authorId;
        Date = DateTimeOffset.Now;
    }

    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public int AuthorId { get; set; }

    //public Author Author { get; set; }

    public DateTimeOffset Date { get; set; }
}