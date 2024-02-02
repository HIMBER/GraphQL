namespace PocGraphQL.Common.Model;

public record BookInput([property: System.ComponentModel.DefaultValue("")] Optional<string> Title, string Author);