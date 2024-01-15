using System.ComponentModel.DataAnnotations.Schema;

namespace PocGraphQL.Common.Model;

[Table("address")]
public class Address
{
    public int Id { get; set; }

    public string StreetName { get; set; }

    public int AuthorId { get; set; }

    public Author Author { get; set; }

    public Address(int id, string streetName, int authorId)
    {
        Id = id;
        StreetName = streetName;
        AuthorId = authorId;
    }
}