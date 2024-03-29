using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;

namespace PocGraphQL.Common.Model;

[Table("book")]
public class Book
{
    public Book()
    {
    }

    public Book(int id, string title, int authorId)
    {
        Id = id;
        Title = title;
        AuthorId = authorId;
        Date = DateTimeOffset.Now;
    }

    public int Id { get; set; }

    [Required] public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public DateTimeOffset Date { get; set; }

    public static CSharpFunctionalExtensions.Result<Book> Create()
    {
        return Result.Success<Book>(default!);
    }
}