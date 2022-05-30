using System.Diagnostics;
using BookCityLibrary.UI.Contracts;

namespace BookCityLibrary.UI.Services;

public class RepositoryService<T> : IRepositoryService<T> where T : class
{
    private readonly HttpClient _client;

    protected RepositoryService(HttpClient client)
    {
        _client = client;
    }

    public async Task<T?> Create(string url, T? entity)
    {
        Debug.Assert(entity != null, nameof(entity) + " != null");
        var response = await _client.PostAsJsonAsync<T>(url, entity);

        return response.StatusCode == System.Net.HttpStatusCode.Created ? entity : null;
    }

    public async Task<bool> Delete(string url, int id)
    {
        if (id < 1)
        {
            return false;
        }

        var response = await _client.DeleteAsync(url + id);

        return response.StatusCode == System.Net.HttpStatusCode.NoContent;
    }

    public async Task<IList<T>?> GetAll(string url)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<IList<T>>(url);

            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IList<T>?> GetBySearch(string url, string search)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<IList<T>>(url + $"?search={search}");

            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<T?> GetSingle(string url, int id)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<T>(url + id);

            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> Update(string url, T? entity, int id)
    {
        if (entity == null)
        {
            return false;
        }

        var response = await _client.PutAsJsonAsync(url + id, entity);

        return response.StatusCode == System.Net.HttpStatusCode.NoContent;
    }
}