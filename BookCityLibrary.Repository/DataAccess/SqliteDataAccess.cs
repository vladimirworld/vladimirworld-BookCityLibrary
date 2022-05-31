using System.Data;
using BookLibrary.Data.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace BookCityLibrary.Repository.DataAccess;

public class SqliteDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqliteDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<T>> LoadData<T, TU>(string sql, TU parameters, string connectionStringName)
    {
        using IDbConnection connection = new SqliteConnection(_config.GetConnectionString(connectionStringName));
        connection.Open();

        var rows = await connection.QueryAsync<T>(sql, parameters, commandType: CommandType.Text);

        return rows.ToList();
    }

    public async Task SaveData<T>(string sql, T parameters, string connectionStringName)
    {
        using IDbConnection connection = new SqliteConnection(_config.GetConnectionString(connectionStringName));
        connection.Open();

        await connection.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
    }
}