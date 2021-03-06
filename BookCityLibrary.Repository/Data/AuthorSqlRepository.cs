using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace BookCityLibrary.Repository.Data;

public class AuthorSqlRepository : IAuthorRepository
{
    private readonly ISqlDataAccess _sqliteData;
    private readonly IConfiguration _config;
    private readonly string connectionString = "sqlite";

    public AuthorSqlRepository(ISqlDataAccess sqliteData, IConfiguration config)
    {
        _sqliteData = sqliteData;
        _config = config;
    }

    public async Task<bool> Create(Author entity)
    {
        const string sql = "INSERT INTO Authors (FirstName, LastName, Bio) VALUES (@FirstName, @LastName, @Bio);";

        try
        {
            var author = new { entity.FirstName, entity.LastName, entity.Bio };

            await _sqliteData.SaveData(sql, author, connectionString);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> Delete(Author entity)
    {
        const string sql = "PRAGMA foreign_keys = OFF; DELETE FROM Authors WHERE Id = @Id";

        try
        {
            await _sqliteData.SaveData(sql, new { entity.Id }, connectionString);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<IList<Author>?> FindAll()
    {
        const string sql = "SELECT Id, FirstName, LastName, Bio FROM Authors; SELECT Id, Title FROM Books;";

        try
        {
            await using var connection = new SqliteConnection(_config.GetConnectionString(connectionString));
            using var multi = await connection.QueryMultipleAsync(sql);
            var authors = multi.Read<Author>().ToList();
            var books = multi.Read<Book>().ToList();

            foreach (var author in authors)
            {
                author.Books = books;
            }

            return authors;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IList<Author>?> FindBySearch(string search)
    {
        const string sql = "SELECT Id, FirstName, LastName FROM Authors WHERE FirstName LIKE @Search UNION " +
                           "SELECT Id, FirstName, LastName FROM Authors WHERE LastName LIKE @Search";
        try
        {
            var results = await _sqliteData.LoadData<Author, dynamic>(sql,
                new { Search = "%" + search + "%" }, connectionString);

            return results.ToList();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Author?> FindById(int id)
    {
        const string sql = "SELECT Id, FirstName, LastName, Bio FROM Authors WHERE Id = @Id;" +
                           " SELECT Id, Title, Summary, Price FROM Books WHERE AuthorId = @Id;";

        try
        {
            await using var connection = new SqliteConnection(_config.GetConnectionString(connectionString));
            using var multi = await connection.QueryMultipleAsync(sql, new { @Id = id });
            var author = multi.Read<Author>().SingleOrDefault();
            var books = multi.Read<Book>().ToList();

            if (author != null)
            {
                author.Books = books;
            }

            return author;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> Update(Author entity)
    {
        const string sql =
            @"UPDATE Authors SET FirstName = @FirstName, LastName = @LastName, Bio = @Bio WHERE Id = @Id";

        try
        {
            await _sqliteData.SaveData(sql, entity, connectionString);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> IsExists(int id)
    {
        const string sql = "SELECT CASE WHEN EXISTS (SELECT Id FROM Authors WHERE Id = @Id)" +
                           "THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS Result";

        try
        {
            await using var connection = new SqliteConnection(_config.GetConnectionString(connectionString));
            var isExists = await connection.QueryFirstAsync<bool>(sql, new { @Id = id });

            return isExists;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}