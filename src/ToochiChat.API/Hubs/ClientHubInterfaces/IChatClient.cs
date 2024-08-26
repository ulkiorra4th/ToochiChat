using ToochiChat.API.RequestModels.Chat;

namespace ToochiChat.API.Hubs.ClientHubInterfaces;

internal interface IChatClient
{
    public Task ReceiveNewMember(string userName, DateTime joinDate);
    public Task ReceiveLeftMember(string userName, DateTime joinDate);
    public Task ReceiveMessage(string userName, string message);
    public Task DeleteMessage(int messageId, string chatId);
    public Task UpdateMessage(int messageId, string chatId, MessageModel message);
}