using BlazorApp.UI.Contracts;
using BlazorApp.UI.Models;

namespace BlazorApp.UI.Services;

public class AuthorService : RepositoryService<Author>, IAuthorRepository
{
    private readonly HttpClient _client;

    public AuthorService(HttpClient client) 
        : base(client)
    {
        _client = client;
    }
}