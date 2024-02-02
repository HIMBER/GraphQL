using System.Reflection;
using HotChocolate.Types.Descriptors;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Api.Queries;

public class UseYourCustomAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        descriptor.Use(next => async context =>
        {
            // before the resolver pipeline
            await next(context);
            // after the resolver pipeline

            if (context.Result is IQueryable<Author> query)
            {
                // all middleware are applied to `query`
            }
        });
    }
}