using System.ComponentModel;
using HotChocolate;

namespace PocGraphQL.Common.Model;

public record BookInput([property: DefaultValue("")] Optional<string> Title, string Author);