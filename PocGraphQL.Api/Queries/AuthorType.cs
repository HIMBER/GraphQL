using PocGraphQL.Api.DataLoaders;
using PocGraphQL.Api.Model;

namespace PocGraphQL.Api.Queries;

public class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Field("books")
            .Resolve(context =>
            {
                var key = context.Parent<Author>().Id;
                var cancellationToken = context.RequestAborted;

                return context.DataLoader<AuthorBooksDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<ListType<BookType>>>();
    }
}