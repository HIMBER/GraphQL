using Microsoft.EntityFrameworkCore;
using PocGraphQL.Api.DbContext;
using PocGraphQL.Api.Model;

namespace PocGraphQL.Api.Helpers;

[ExtendObjectType<Author>]
public static class AuthorHelper
{
    [DataLoader]
    internal static async Task<IList<Author>> GetAuthorByNameAsync(
        string name,
        LibraryContext context,
        CancellationToken cancellationToken) =>
        await context.Authors
        .Where(a => a.Name.StartsWith(name))
        .ToListAsync(cancellationToken: cancellationToken);
}