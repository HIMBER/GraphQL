using HotChocolate;
using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.DataLoaders;

public static class LibraryDataLoader
{
    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, Author>> GetAuthorsByIdAsync(
        IReadOnlyList<int> keys,
        LibraryContext context,
        CancellationToken cancellationToken)
    {
        return await context.Authors.Where(author => keys.Contains(author.Id))
            .ToDictionaryAsync(author => author.Id, cancellationToken);
    }

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, Book>> GetBooksByAuthorIdAsync(
        IReadOnlyList<int> keys,
        LibraryContext context,
        CancellationToken cancellationToken)
    {
        var books = await context.Books.Where(book => keys.Contains(book.AuthorId)).ToListAsync(cancellationToken);
        return books.ToDictionary(book => book.AuthorId);
    }
}