using HotChocolate.Caching;
using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

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
*/
    [CacheControl(10_000, Scope = CacheControlScope.Public)]
    public Book GetSampleBook2() =>
        new Book(
            1,
            "Book2", 5);

    /*public async Task<Author> GetAuthorByNameAsync(
        string name,
        IAuthorByNameDataLoader authorByName,
        CancellationToken cancellationToken) => */

    public IQueryable<Author> GetAuthors(LibraryContext context) => context.Authors;
    public IQueryable<Book> GetBooks(LibraryContext context) => context.Books;
}