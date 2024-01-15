using HotChocolate.Types;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.Types;

public class AddressType : ObjectType<Address>
{
    protected override void Configure(IObjectTypeDescriptor<Address> descriptor)
    {
        descriptor.Field(address => address.AuthorId) // Remplace le champ AuthorId par le champ author
            .Name("author")
            .Resolve(async context =>
            {
                var key = context.Parent<Address>().AuthorId;
                var cancellationToken = context.RequestAborted;

                return await context.DataLoader<AuthorsByIdDataLoader>().LoadAsync(key, cancellationToken);
            })
            .Type<NonNullType<AuthorType>>();
    }
}