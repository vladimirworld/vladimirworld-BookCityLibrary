namespace BookCityLibrary.UI.Contracts;

public interface IRepositoryService<T> where T : class
{
    Task<IList<T>?> GetAll(string url);
        
    Task<IList<T>?> GetBySearch(string url, string search);
        
    Task<T?> GetSingle(string url, int id);
        
    Task<T?> Create(string url, T? entity);
        
    Task<bool> Update(string url, T? entity, int id);
        
    Task<bool> Delete(string url, int id);
}