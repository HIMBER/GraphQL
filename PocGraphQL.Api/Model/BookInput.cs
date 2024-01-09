namespace PocGraphQL.Api.Model;

public record BookInput([property: DefaultValue("")] Optional<string> Title, string Author);