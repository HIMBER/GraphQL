using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Api.DbContext;

public static class DatabaseSeeder
{
   public static async Task SeedDatabaseIfNecessary(WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        await using var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();

        await context.Database.MigrateAsync();

        if (!await context.Authors.AnyAsync())
        {
            await context.Authors.AddRangeAsync(new List<Author>()
            {
                new(1, "Zebra3"),
                new(2, "Kobrakan"),
                new(3, "SonGoku"),
                new(4, "Vegeta")
            });

            await context.SaveChangesAsync();
        }

        if (!await context.Books.AnyAsync())
        {
            await context.Books.AddRangeAsync(new List<Book>()
            {
                new(1, "Le .Net y'a pas mieux", 1),
                new(2, "Vieux souvenir", 2),
                new(3, "La vie des Sayans", 3),
                new(4, "Toujours gagner c'est fatigant", 3),
                new(5, "Perdre c'est un peu gagner", 4),
                new(6, "Je l'aime pas l'autre là", 4),
                new(7, "Il est trop fort", 4),
                new(8, "C'est gavant", 4),
            });

            await context.SaveChangesAsync();
        }
    }
}