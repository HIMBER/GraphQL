namespace PocGraphQL.Common.DTOs;

public class AuthorDTO
{
    public int Id { get; set; }
    
    public string FullName { get; set; }
    
    public virtual AddressDTO? AuthorAddress { get; set; }
}