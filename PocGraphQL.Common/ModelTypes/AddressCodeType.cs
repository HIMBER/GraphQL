using HotChocolate.Data.Filters;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.ModelTypes;

public class AddressCodeType : ObjectType<AddressCode>
{
    protected override void Configure(IObjectTypeDescriptor<AddressCode> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(f => f.Value).Type<StringType>();
    }
}

public class AddressCodeFilter : FilterInputType<AddressCode>
{
    protected override void Configure(IFilterInputTypeDescriptor<AddressCode> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(f => f.Value);
    }
}