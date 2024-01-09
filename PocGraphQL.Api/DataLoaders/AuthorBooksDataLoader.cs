using Microsoft.EntityFrameworkCore;
using PocGraphQL.Api.DbContext;
using PocGraphQL.Api.Model;

namespace PocGraphQL.Api.DataLoaders;

public class AuthorBooksDataLoader : GroupedDataLoader<int, Book> // Relation un à plusieurs
{
    private readonly LibraryContext _context;
    
    public AuthorBooksDataLoader(IBatchScheduler batchScheduler, LibraryContext context, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _context = context;
    }

    protected override async Task<ILookup<int, Book>> LoadGroupedBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        var books = await _context.Books.Where(book => keys.Contains(book.AuthorId)).ToListAsync(cancellationToken);
        return books.ToLookup(book => book.AuthorId);
    }
}