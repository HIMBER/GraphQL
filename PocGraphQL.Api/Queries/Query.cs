using HotChocolate.Caching;
using PocGraphQL.Api.DbContext;
using PocGraphQL.Api.Model;

namespace PocGraphQL.Api.Queries;

public class Query
{
    /*public Book GetSampleBook() =>
        new Book
        {
            Title = "C# in depth",
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };

    public Book GetSampleBookByName(string bookName) =>
        new Book
        {
            Title = bookName,
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };

    [CacheControl(10_000, Scope = CacheControlScope.Public)]
    public Book GetSampleBook2() =>
        new Book
        {
            Title = "Book2",
            Date = DateTimeOffset.Now,
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };*/
    
    /*public async Task<Author> GetAuthorByNameAsync(
        string name,
        IAuthorByNameDataLoader authorByName,
        CancellationToken cancellationToken) => */

    public IQueryable<Author> GetAuthors(LibraryContext context) => context.Authors;
    public IQueryable<Book> GetBooks(LibraryContext context) => context.Books;
}