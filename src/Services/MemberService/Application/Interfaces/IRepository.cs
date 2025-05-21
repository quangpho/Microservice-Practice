namespace Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetItemAsync(string id);
    Task AddItemAsync(T item);
    Task UpdateItemAsync(T item);
    Task DeleteItemAsync(string id);
}