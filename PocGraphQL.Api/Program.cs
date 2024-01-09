using Microsoft.EntityFrameworkCore;
using PocGraphQL.Api.DataLoaders;
using PocGraphQL.Api.DbContext;
using PocGraphQL.Api.Model;
using PocGraphQL.Api.Queries;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

/*builder.Services.AddPooledDbContextFactory<LibraryContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));*/

builder.Services.AddHttpContextAccessor();

builder.Services.AddGraphQLServer()
    .InitializeOnStartup()
    .AddDataLoader<AuthorBooksDataLoader>()
    .AddDataLoader<AuthorDataLoader>()
    .AddQueryType<Query>()
    .AddType<AuthorType>()
    .AddType<BookType>()
    .RegisterDbContext<LibraryContext>(DbContextKind.Resolver);
// .AddCacheControl()
    // .UseQueryCachePipeline();
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