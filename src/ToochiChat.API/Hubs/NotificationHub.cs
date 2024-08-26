using Microsoft.AspNetCore.SignalR;
using ToochiChat.API.Hubs.ClientHubInterfaces;

namespace ToochiChat.API.Hubs;

public sealed class NotificationHub : Hub<INotificationClient>
{
    
}