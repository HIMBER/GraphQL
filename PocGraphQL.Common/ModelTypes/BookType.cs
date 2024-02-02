using HotChocolate.Data.Filters;
using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.ModelTypes;

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(f => f.Id).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(f => f.Title).Type<StringType>();
        descriptor.Field(f => f.Author) // Remplace le champ AuthorId par le champ author
            .Resolve(async context =>
            {
                var key = context.Parent<Book>().AuthorId;
                var cancellationToken = context.RequestAborted;
                return await context.DataLoader<AuthorsByAuthorIdDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<AuthorType>>();
    }

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, Author>> GetAuthorsByAuthorIdAsync(
        IReadOnlyList<int> keys,
        ApiContext context,
        CancellationToken cancellationToken)
    {
        return await context.Authors.Where(author => keys.Contains( author.Id)).ToDictionaryAsync(author => author.Id, cancellationToken);
    }
}

public class BookFilter : FilterInputType<Book>
{
    protected override void Configure(IFilterInputTypeDescriptor<Book> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(f => f.Id);
        descriptor.Field(f => f.Title);
        descriptor.Field(f => f.Author);
    }
}