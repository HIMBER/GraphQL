using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.DbContext;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Api.Database;

public static class DatabaseSeeder
{
    public static async Task SeedDatabaseIfNecessary(WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        await using var context = scope.ServiceProvider.GetRequiredService<ApiContext>();

        await context.Database.MigrateAsync();

        if (!await context.Authors.AnyAsync())
        {
            await context.Authors.AddRangeAsync(new List<Author>()
            {
                new(1, "Isabelle Renard"),
                new(2, "Alexandre Noir"),
                new(3, "Élise Montagne"),
                new(4, "Olivier Sylver"),
                new(5, "Nathalie Astral")
            });

            await context.SaveChangesAsync();
        }
        
        if (!await context.Addresses.AnyAsync())
        {
            await context.Addresses.AddRangeAsync(new List<Address>()
            {
                new(1, "Rue des Mystères", 1, "AAA"),
                new(2, "Avenue de l'Aurore", 2, "BBB"),
                new(3, "Chemin des Étoiles", 3, "CCC"),
                new(4, "Rue des Sylves", 4, "DDD"),
                new(5, "Boulevard de la Nébuleuse", 5, "EEE")
            });

            await context.SaveChangesAsync();
        }

        if (!await context.Books.AnyAsync())
        {
            await context.Books.AddRangeAsync(new List<Book>()
            {
                new(1, "L'Éclipse du Crépuscule", 1),
                new(2, "Les Secrets de l'Éclat Lunaire", 2),
                new(3, "Le Voyageur Éternel", 3),
                new(4, "Les Murmures du Silmar", 3),
                new(5, "Les Chroniques de l'Orbe Céleste", 4),
                new(6, "L'Ombre du Phénix", 4),
                new(7, "Les Échos du Cosmos", 4),
                new(8, "Le Sceptre des Étoiles", 4),
            });

            await context.SaveChangesAsync();
        }
    }
}