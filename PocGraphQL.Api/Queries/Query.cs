using System.Diagnostics;
using System.Reflection;
using HotChocolate.Caching;
using HotChocolate.Types.Descriptors;
using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;
using PocGraphQL.Common.Telemetry;

namespace PocGraphQL.Api.Queries;

public class Query
{
    private static readonly ActivitySource MyActivitySource = new("OpenTelemetry.Demo.Jaeger");

    private readonly DiagnosticConfig _diagnosticConfig;

    public Query(DiagnosticConfig diagnosticConfig)
    {
        _diagnosticConfig = diagnosticConfig;
    }

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Author> GetAuthors(ApiContext context) => context.Authors.AsNoTracking();

    /*[UseFiltering]
    [UseSorting]
    public IQueryable<AuthorDTO> GetAuthorsDTO(ApiContext context, [Service] IMapper mapper)
    {
        var authors = context.Authors;
        return  mapper.Map<IEnumerable<AuthorDTO>>(authors).AsQueryable();
    }*/
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book> GetBooks(ApiContext context) => context.Books;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Address> GetAddresses(ApiContext context) => context.Addresses.AsNoTracking();

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
}

public class UseYourCustomAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.Use(next => async context =>
        {
            // before the resolver pipeline
            await next(context);
            // after the resolver pipeline

            if (context.Result is IQueryable<Author> query)
            {
                // all middleware are applied to `query`
            }
        });
    }
}