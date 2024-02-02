using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PocGraphQL.Common.Model;

[Table("address")]
public class Address : IEntityTypeConfiguration<Address>
{
    public int Id { get; set; }

    public string StreetName { get; set; }

    public int AuthorId { get; set; }

    public AddressCode Code { get; }

    public virtual Author Author { get; set; }

    public Address()
    {
    }

    public Address(int id, string streetName, int authorId, AddressCode code)
    {
        Id = id;
        StreetName = streetName;
        AuthorId = authorId;
        Code = code;
    }

    public static CSharpFunctionalExtensions.Result<Address> Create()
    {
        return Result.Success<Address>(default!);
    }

    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("address", "public");
        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.HasKey(b => b.Id);
        builder.OwnsOne(e => e.Code).Property(p => p.Value).HasColumnName("Code");
    }
}