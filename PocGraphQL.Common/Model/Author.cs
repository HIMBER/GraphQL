using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSharpFunctionalExtensions;
using PocGraphQL.Common.Attributes;
using PocGraphQL.Common.Extensions;

namespace PocGraphQL.Common.Model;

[Table("author")]
public class Author 
{
    public Author()
    {
    }
    
    public Author(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public int Id { get; set; }
    
    [Required]
    [UseUpperCase]
    public string Name { get; set; }
    
    public virtual Address Address { get; set; }
    
    public ICollection<Book> Books { get; set; }

    public int VersionNumber { get; set; } = 2;

    public string UseLessValue { get; set; } = "Useless";

    public AuthorEnum AuthorEnum { get; set; }
    
    public CSharpFunctionalExtensions.Result<Author> SetName(string name)
    {
        Name = name;
        return this.Success();
    }

    public static CSharpFunctionalExtensions.Result<Author> Create()
    {
        return Result.Success<Author>(default!);
    }
}