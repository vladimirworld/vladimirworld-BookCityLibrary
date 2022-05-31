using System.Net.Http;
using BookCityLibrary.UI.Contracts;
using BookCityLibrary.UI.Models;

namespace BookCityLibrary.UI.Services;

public class AuthorService : RepositoryService<Author>, IAuthorRepository
{
    private readonly HttpClient _client;

    public AuthorService(HttpClient client) 
        : base(client)
    {
        _client = client;
    }
}