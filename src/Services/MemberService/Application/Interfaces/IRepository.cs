namespace Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetItemAsync(string id, string partitionKey);
    Task AddItemAsync(T item, string partitionKey);
    Task AddItemAsync(T item);
    Task UpdateItemAsync(T item, string partitionKey);
    Task DeleteItemAsync(string id, string partitionKey);
}