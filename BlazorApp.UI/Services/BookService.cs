using BlazorAppTest.Contracts;
using BlazorAppTest.Models;

namespace BlazorAppTest.Services;

public class BookService : RepositoryService<Book>, IBookRepository
{
    private readonly HttpClient _client;

    public BookService(HttpClient client) 
        : base(client)
    {
        _client = client;
    }
}