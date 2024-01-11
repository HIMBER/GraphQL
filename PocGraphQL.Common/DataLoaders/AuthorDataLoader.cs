using GreenDonut;
using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.DataLoaders;

public class AuthorDataLoader : BatchDataLoader<int, Author> // Relation un à un
{
    private readonly LibraryContext _context;
    
    public AuthorDataLoader(IBatchScheduler batchScheduler, LibraryContext context, DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _context = context;
    }

    protected override async Task<IReadOnlyDictionary<int, Author>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
    {
        var authors = await _context.Authors.Where(author => keys.Contains(author.Id)).ToListAsync(cancellationToken);
        return authors.ToDictionary(author => author.Id);
    }
}