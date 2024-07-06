namespace ToochiChat.API.ClientHubInterfaces;

internal interface IChatClient
{
    public Task ReceiveMessage(string userName, string message);
}