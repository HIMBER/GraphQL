using GreenDonut;
using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.DataLoaders;

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