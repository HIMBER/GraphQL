using HotChocolate.Data.Filters;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.ModelTypes;

public class AddressType : ObjectType<Address>
{
    protected override void Configure(IObjectTypeDescriptor<Address> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(f => f.Id).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(f => f.AuthorId).Type<NonNullType<IntType>>().IsProjected();
        descriptor.Field(f => f.StreetName).Type<NonNullType<StringType>>();
        descriptor.Field(address => address.Author) // Remplace le champ AuthorId par le champ author
            .Resolve(async context =>
            {
                var key = context.Parent<Address>().AuthorId;
                var cancellationToken = context.RequestAborted;

                return await context.DataLoader<AuthorsByAuthorIdDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<AuthorType>>();

        descriptor.Field(f => f.Code)
            .Type<AddressCodeType>()
            .IsProjected();
        /*descriptor.Field("codeAddress")
            .Resolve(context => ValueTask.FromResult<object?>(context.Parent<Address>().Code))
            .Type<StringType>();*/

        descriptor.Field("code2")
            .Resolve(context => context.Parent<Address>().Code.Value)
            .Type<StringType>();
    }
}

public class AddressFilter : FilterInputType<Address>
{
    protected override void Configure(IFilterInputTypeDescriptor<Address> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(f => f.Id);
        descriptor.Field(f => f.AuthorId);
        descriptor.Field(f => f.StreetName);
        descriptor.Field(address => address.Author).Type<AuthorFilter>();
        //descriptor.Field("code2");
        //descriptor.Field(f => f.Code.Value).Name("code2");//.Type<AddressCodeFilter>();

        //descriptor.Field("addressCode");
        //descriptor.Field("codeAddress");
    }
}