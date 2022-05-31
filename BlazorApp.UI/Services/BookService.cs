using BlazorApp.UI.Contracts;
using BlazorApp.UI.Models;

namespace BlazorApp.UI.Services;

public class BookService : RepositoryService<Book>, IBookRepository
{
    private readonly HttpClient _client;

    public BookService(HttpClient client) 
        : base(client)
    {
        _client = client;
    }
}