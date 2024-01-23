using System.Diagnostics;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PocGraphQL.Api;
using PocGraphQL.Api.Database;
using PocGraphQL.Api.Queries;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Telemetry;
using PocGraphQL.Common.Types;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddSingleton<DiagnosticConfig>();

/*builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));*/

builder.Services.AddDbContext<ApiContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Trace));

/*builder.Services.AddDbContextFactory<LibraryContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));*/

/*builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Microsoft.AspNetCore.Hosting");
        metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        metrics.AddMeter("System.Net.Http");

        // Env variable
        // OTEL_EXPORTER_OTLP_ENDPOINT
        metrics.AddOtlpExporter();
    });*/
    
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeFormattedMessage = true;
    logging.IncludeScopes = false;

    var resourceBuilder = ResourceBuilder
        .CreateDefault()
        .AddService(builder.Environment.ApplicationName);

    logging.SetResourceBuilder(resourceBuilder)

        // ConsoleExporter is used for demo purpose only.
        // In production environment, ConsoleExporter should be replaced with other exporters (e.g. OTLP Exporter).
        //.AddConsoleExporter()
        // .AddOtlpExporter( o => o.Endpoint = new Uri("http://otel-collector:4317"));
        .AddOtlpExporter( o => o.Endpoint = new Uri("http://otel-collector:5317"));
});

builder.Services.AddOpenTelemetry()
    .WithTracing(traceProviderBuilder =>
        traceProviderBuilder
            .AddNpgsql()
            .AddEntityFrameworkCoreInstrumentation()
            .AddOtlpExporter());

/*builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder

            .AddSource(DiagnosticsConfig.ActivitySource.Name)
            .ConfigureResource(resource =>
                resource.AddService(DiagnosticsConfig.ServiceName))
            .AddHttpClientInstrumentation()
            .AddAspNetCoreInstrumentation();*/

builder.Services.AddHttpContextAccessor();

builder.Services.AddGraphQLServer()
    .InitializeOnStartup()
    .RegisterDbContext<ApiContext>(DbContextKind.Resolver)
    .AddErrorFilter<GraphQLErrorFilter>()
    .AddQueryType<Query>()
    .AddTypeExtension<AuthorType>()
    .AddTypeExtension<BookType>()
    .AddTypeExtension<AddressType>()
    .SetPagingOptions(new PagingOptions()
    {
        MaxPageSize = 50,
        DefaultPageSize = 50,
        IncludeTotalCount = true
    })
    .AddProjections()
    .AddFiltering()
    .AddSorting()
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


