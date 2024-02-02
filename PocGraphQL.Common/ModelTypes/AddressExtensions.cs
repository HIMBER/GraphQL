using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.ModelTypes;

[ExtendObjectType<Address>]
public sealed class AddressExtensions
{
    public string GetAddressCode([Parent] Address address) => address.Code.Value;
}