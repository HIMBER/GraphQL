using PocGraphQL.Common.DataLoaders;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Api.Queries;

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

                return await context.DataLoader<AuthorDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<AuthorType>>();
    }
}