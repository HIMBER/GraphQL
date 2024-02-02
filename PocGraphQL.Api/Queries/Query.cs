using System.Diagnostics;
using HotChocolate.Caching;
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

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book> GetBooks(ApiContext context) => context.Books;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Address> GetAddresses(ApiContext context) => context.Addresses.AsNoTracking();

    [CacheControl(10_000, Scope = CacheControlScope.Public)]
    public Book GetSampleBook2() =>
        new Book(
            1,
            "Book2", 5);
}