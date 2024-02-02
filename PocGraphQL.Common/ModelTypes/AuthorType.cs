using HotChocolate.Data.Filters;
using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.ModelTypes;

public class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Description("Auteur de livres");
        descriptor.Field(f => f.Id).Type<NonNullType<IntType>>().IsProjected(true);
        descriptor.Field(f => f.Name).Type<StringType>().Description("Author Name");
        descriptor.Field("rating").Resolve(context => 5).Type<NonNullType<IntType>>();
        descriptor.Field(f => f.AuthorEnum).Type<NonNullType<AuthorEnumType>>();
        
        descriptor.Field(f => f.Address) // Ajoute le champ address
            .Resolve(context =>
            {
                var key = context.Parent<Author>().Id;
                var cancellationToken = context.RequestAborted;

                return context.DataLoader<AddressByAuthorIdDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<AddressType>>()
            .UseFiltering();

        descriptor.Field(f => f.Books) // Ajoute le champ books
            .Resolve(context =>
            {
                var key = context.Parent<Author>().Id;
                var cancellationToken = context.RequestAborted;

                return context.DataLoader<BooksByAuthorIdDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<ListType<BookType>>>()
            .UsePaging<NonNullType<BookType>>()
            .UseFiltering<BookFilter>();

        descriptor.Field("adresse").Resolve(context => context.Parent<Author>().Address.StreetName);
    }

    [DataLoader]
    internal static Task<ILookup<int, Book>> GetBooksByAuthorIdAsync(
        IReadOnlyList<int> keys,
        ApiContext context) =>
        Task.FromResult(context.Books.Where(book => keys.Contains(book.AuthorId)).ToLookup(book => book.AuthorId));

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<int, Address>> GetAddressByAuthorIdAsync(
        IReadOnlyList<int> keys,
        ApiContext context) =>
        await context.Addresses.Where(address => keys.Contains( address.AuthorId)).ToDictionaryAsync(address => address.Id);
}

public class AuthorFilter : FilterInputType<Author>
{
    protected override void Configure(IFilterInputTypeDescriptor<Author> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(f => f.Id);
        descriptor.Field(f => f.Name);
        //descriptor.Field("rating").Ignore();//.Type<RatingFilter>();
        descriptor.Field(f => f.AuthorEnum);
        descriptor.Field(f => f.Address).Type<AddressFilter>();
        descriptor.Field(f => f.Books);
        //descriptor.Field("adresse").Ignore();
    }
}

public class RatingFilter : FilterInputType<RatingType>
{
    protected override void Configure(IFilterInputTypeDescriptor<RatingType> descriptor)
    {
        descriptor.Field(f => f);
    }
}

public class RatingType : ObjectType<int>
{
    protected override void Configure(IObjectTypeDescriptor<int> descriptor)
    {
        descriptor.Field(f => f).Type<NonNullType<IntType>>();
    }
}