using HotChocolate;
using HotChocolate.Types;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.Types;

public class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Field("books") // Ajoute le champ books
            .Resolve(context =>
            {
                var key = context.Parent<Author>().Id;
                var cancellationToken = context.RequestAborted;

                return context.DataLoader<BooksByAuthorIdDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<ListType<BookType>>>();
    }
    
    [DataLoader]
    internal static Task<ILookup<int, Book>> GetBooksByAuthorIdAsync(
        IReadOnlyList<int> keys,
        LibraryContext context) =>
        Task.FromResult(context.Books.Where(book => keys.Contains(book.AuthorId)).ToLookup(book => book.AuthorId));
}