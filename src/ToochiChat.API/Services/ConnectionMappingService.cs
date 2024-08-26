using ToochiChat.API.Services.Interfaces;

namespace ToochiChat.API.Services;

internal sealed class ConnectionMappingService<T> : IConnectionMappingService<T> where T : notnull 
{
    // TODO: use redis
    private readonly Dictionary<T, HashSet<string>?> _connections = new();

    public int Count => _connections.Count;

    public void Add(T key, string connectionId)
    {
        if (!_connections.TryGetValue(key, out var connections)) 
        { 
            connections = new HashSet<string>(); 
            _connections.Add(key, connections);
        }
            
        connections!.Add(connectionId);
    }

    public IEnumerable<string> GetConnections(T key)
    {
        return _connections.TryGetValue(key, out var connections) 
            ? connections! 
            : Enumerable.Empty<string>();
    }

    public void RemoveConnection(T key, string connectionId)
    {
        if (!_connections.TryGetValue(key, out var connections)) return;
        if (connections!.Contains(connectionId)) connections.Remove(connectionId);
        if (connections.Count == 0) _connections.Remove(key);
    }

    public void RemoveUser(T key)
    {
        if (!_connections.TryGetValue(key, out var connections)) return;
        _connections.Remove(key);
    }

    public bool UserExists(T key)
    {
        return _connections.ContainsKey(key);
    }
}