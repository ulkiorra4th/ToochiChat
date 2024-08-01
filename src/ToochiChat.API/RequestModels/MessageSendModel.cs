namespace ToochiChat.API.RequestModels;

public sealed class MessageSendModel
{
    public required string AccountId { get; set; }
    public required MessageModel Message { get; set; }
}