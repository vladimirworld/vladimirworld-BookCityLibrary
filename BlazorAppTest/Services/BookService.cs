using BookCityLibrary.UI.Contracts;
using BookCityLibrary.UI.Models;

namespace BookCityLibrary.UI.Services;

public class BookService : RepositoryService<Book>, IBookRepository
{
    private readonly HttpClient _client;

    public BookService(HttpClient client) 
        : base(client)
    {
        _client = client;
    }
}