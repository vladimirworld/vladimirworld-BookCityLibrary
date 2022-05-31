using System.Diagnostics.CodeAnalysis;

namespace BookCityLibrary.Api.Dtos;

[ExcludeFromCodeCoverage]
public class AuthorDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Bio { get; set; }
}