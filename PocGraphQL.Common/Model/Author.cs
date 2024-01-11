using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate;

namespace PocGraphQL.Common.Model;

[GraphQLName("bookAuthor")]
[Table("author")]
public class Author 
{
    public Author(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    
    [GraphQLName("fullName")]
    [Required]
    public string Name { get; set; }

    //public ICollection<Book> Books { get; set; }
}