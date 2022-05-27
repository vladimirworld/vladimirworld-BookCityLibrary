using BookCityLibrary.Contracts;
using BookCityLibrary.Models;

namespace BookCityLibrary.Services;

public class AuthorService : RepositoryService<Author>, IAuthorRepository
{
    private readonly HttpClient _client;

    public AuthorService(HttpClient client) 
        : base(client)
    {
        _client = client;
    }
}