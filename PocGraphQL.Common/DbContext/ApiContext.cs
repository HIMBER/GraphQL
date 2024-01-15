using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.DbContext;

public class ApiContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Address> Addresses => Set<Address>();
}