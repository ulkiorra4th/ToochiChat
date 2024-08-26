namespace ToochiChat.API.RequestModels.Chat;

public class MessageModel
{
    public required string Text { get; set; }
    public required DateTime SentDate { get; set; }
    public required DateTime EditDate { get; set; }
    public bool IsEdited { get; set; }
}