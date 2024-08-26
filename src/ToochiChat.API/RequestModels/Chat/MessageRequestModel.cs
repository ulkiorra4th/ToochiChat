namespace ToochiChat.API.RequestModels.Chat;

public sealed class MessageRequestModel
{
    public required string UserName { get; set; }
    public required string ChatId { get; set; }
    public required MessageModel Message { get; set; }
}