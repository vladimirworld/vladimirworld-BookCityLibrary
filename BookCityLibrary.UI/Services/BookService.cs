using BookCityLibrary.Contracts;
using BookCityLibrary.Models;

namespace BookCityLibrary.Services;

public class BookService : RepositoryService<Book>, IBookRepository
{
    private readonly HttpClient _client;

    public BookService(HttpClient client) 
        : base(client)
    {
        _client = client;
    }
}