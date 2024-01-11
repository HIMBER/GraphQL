using Microsoft.EntityFrameworkCore;
using PocGraphQL.Api.DbContext;
using PocGraphQL.Api.Queries;
using PocGraphQL.Common.DataLoaders;
using PocGraphQL.Common.DbContext;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddDbContextPool<LibraryContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddGraphQLServer()
    .InitializeOnStartup()
    .RegisterDbContext<LibraryContext>(DbContextKind.Resolver)
    .AddDataLoader<AuthorBooksDataLoader>()
    .AddDataLoader<AuthorDataLoader>()
    .AddQueryType<Query>()
    .AddType<AuthorType>()
    .AddType<BookType>()
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