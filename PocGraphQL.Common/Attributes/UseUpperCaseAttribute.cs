using System.Reflection;
using HotChocolate.Types.Descriptors;
using PocGraphQL.Common.Extensions;

namespace PocGraphQL.Common.Attributes;

public class UseUpperCaseAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure(
        IDescriptorContext context,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.UseUpperCase();
    }
}