using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DataLoaders;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Api.Queries;

public class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Field("books") // Ajoute le champ books
            .Resolve(context =>
            {
                var key = context.Parent<Author>().Id;
                var cancellationToken = context.RequestAborted;

                return context.DataLoader<AuthorBooksDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<ListType<BookType>>>();
    }
}

[ExtendObjectType<Author>]
public static class AuthorNode
{
    [DataLoader(ServiceScope = DataLoaderServiceScope.DataLoaderScope)]
    internal static async Task<IReadOnlyDictionary<int, Author>> GetAuthorById(
        IReadOnlyList<int> keys,
        LibraryContext context,
        CancellationToken cancellationToken) =>
        await context.Authors.Where(author => keys.Contains(author.Id)).ToDictionaryAsync(author => author.Id, cancellationToken);
}