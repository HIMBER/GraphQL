using Microsoft.EntityFrameworkCore;
using PocGraphQL.Api.DbContext;
using PocGraphQL.Api.Queries;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Types;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApiContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

/*builder.Services.AddDbContextFactory<LibraryContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));*/

builder.Services.AddHttpContextAccessor();

builder.Services.AddGraphQLServer()
    .InitializeOnStartup()
    .RegisterDbContext<ApiContext>(DbContextKind.Resolver)
    .AddQueryType<Query>()
    .AddTypeExtension<AuthorType>()
    .AddTypeExtension<BookType>()
    .AddTypeExtension<AddressType>()
    .AddCacheControl()
    .UseQueryCachePipeline();
/*.UsePersistedQueryPipeline()
.AddReadOnlyFileSystemQueryStorage("./persisted_queries");*/

var app = builder.Build();
app.MapGraphQL();


if (app.Environment.IsDevelopment())
{
    await DatabaseSeeder.SeedDatabaseIfNecessary(app);
}

app.UseHttpsRedirection();

app.Run();