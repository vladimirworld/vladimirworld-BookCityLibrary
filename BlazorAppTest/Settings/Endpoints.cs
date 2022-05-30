namespace BookCityLibrary.UI.Settings;

public static class Endpoints
{
    private const string BaseUrl = "https://localhost:5001";
    public static readonly string AuthorsEndpoint = $"{BaseUrl}/api/authors/";
    public static readonly string BooksEndpoint = $"{BaseUrl}/api/books/";
}