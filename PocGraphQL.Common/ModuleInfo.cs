using HotChocolate;

[assembly: Module("Types")]
[assembly: DataLoaderDefaults(
    ServiceScope = DataLoaderServiceScope.DataLoaderScope,
    AccessModifier = DataLoaderAccessModifier.Internal)]