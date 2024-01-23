namespace PocGraphQL.Api;

public class GraphQLErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        var errorMessage = string.Empty;
        if (error.Exception != null)
        {
            errorMessage = error.Exception.GetBaseException().Message;
        }
        if (string.IsNullOrEmpty(errorMessage))
        {
            errorMessage = error.Message;
        }

        return error.WithMessage(errorMessage);
    }
}