using CSharpFunctionalExtensions;

namespace PocGraphQL.Common.Extensions;

public static class EntityExtensions
{
    public static CSharpFunctionalExtensions.Result<T> Success<T>(this T target)
    {
        return Result.Success(target);
    }
}