namespace ToochiChat.API.Services.Interfaces;

public interface IConnectionMappingService<T> where T: notnull
{
    int Count { get; }
    void Add(T key, string connectionId);
    IEnumerable<string> GetConnections(T key);
    void RemoveConnection(T key, string connectionId);
    void RemoveUser(T key);
    bool UserExists(T key);
}