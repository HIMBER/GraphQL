using Microsoft.EntityFrameworkCore;
using PocGraphQL.Common.Model;

namespace PocGraphQL.Common.DbContext;

public class LibraryContext : Microsoft.EntityFrameworkCore.DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Address> Addresses => Set<Address>();

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            _configuration.GetConnectionString("ConfigDefaultConnection"));

        base.OnConfiguring(optionsBuilder);
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        
        modelBuilder.Entity<Author>()
            .ToTable("author")
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Book>()
            .ToTable("book")
            .HasKey(b => b.Id);
        
        modelBuilder.Entity<Address>()
            .ToTable("address")
            .HasKey(b => b.Id);
        
        /*modelBuilder.Entity<Author>()
            .HasOne(a => a.Address)
            .WithOne(b => b.Author)
            .HasForeignKey<Author>(b => b.AddressId);*/
        
        /*// Mapping for Author entity
        modelBuilder.Entity<Author>()
            .ToTable("author")
            .HasKey(a => a.Id);

        modelBuilder.Entity<Author>()
            .Property(a => a.Id)
            .IsRequired();

        modelBuilder.Entity<Author>()
            .Property(a => a.Name)
            .IsRequired()
            .HasColumnName("fullName");

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);

        // Mapping for Book entity
        modelBuilder.Entity<Book>()
            .ToTable("book")
            .HasKey(b => b.Id);

        modelBuilder.Entity<Book>()
            .Property(b => b.Id)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(b => b.Date)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);*/

        base.OnModelCreating(modelBuilder);
    }
}