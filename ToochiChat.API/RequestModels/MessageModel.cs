namespace ToochiChat.API.RequestModels;

public class MessageModel
{
    public required string Content { get; set; }
    public required DateTime CreationDate { get; set; }
}