using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.Types;

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Field(book => book.AuthorId) // Remplace le champ AuthorId par le champ author
            .Name("author")
            .Resolve(async context =>
            {
                var key = context.Parent<Book>().AuthorId;
                var cancellationToken = context.RequestAborted;

                return await context.DataLoader<AuthorsByIdDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<AuthorType>>();
    }
    
    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, Author>> GetAuthorsByIdAsync(
        IReadOnlyList<int> keys,
        LibraryContext context,
        CancellationToken cancellationToken)
    {
        return await context.Authors.Where(author => keys.Contains(author.Id))
            .ToDictionaryAsync(author => author.Id, cancellationToken);
    }
}