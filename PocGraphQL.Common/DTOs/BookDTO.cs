namespace PocGraphQL.Common.DTOs;

public class BookDTO
{
    public int Id { get; set; }
    
    public string BookTitle { get; set; }
    
    public DateTimeOffset Date { get; set; }
}