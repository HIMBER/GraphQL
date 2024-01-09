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

    public IQueryable<Author> GetAuthor(LibraryContext context) => context.Authors;
    public IQueryable<Book> GetBook(LibraryContext context) => context.Books;
}

public class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Field("Books")
            .Resolve(context =>
            {
                var id = context.Parent<Author>().Id;

                return context.Service<LibraryContext>().Books.Where(book => book.AuthorId == id);
            })
            .Serial()
            .Type<NonNullType<ListType<BookType>>>();
    }
}

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Field(book => book.AuthorId)
            .Name("author")
            .Resolve(async context =>
            {
                var keyValues = new object[] { context.Parent<Book>().AuthorId };
                var cancellationToken = context.RequestAborted;

                return await context.Service<LibraryContext>().Authors.FindAsync(keyValues, cancellationToken);
            })
            .Serial()
            .Type<NonNullType<AuthorType>>();
    }
}