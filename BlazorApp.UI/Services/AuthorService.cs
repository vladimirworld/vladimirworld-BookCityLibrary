using BlazorAppTest.Contracts;
using BlazorAppTest.Models;

namespace BlazorAppTest.Services;

public class AuthorService : RepositoryService<Author>, IAuthorRepository
{
    private readonly HttpClient _client;

    public AuthorService(HttpClient client) 
        : base(client)
    {
        _client = client;
    }
}