namespace ToochiChat.API.RequestModels.Chat;

public sealed class MessageModel
{
    public required string Text { get; set; }
    public List<FileModel> Media { get; set; } = new();
    public required DateTime SentDate { get; set; }
    public required DateTime EditDate { get; set; }
    public bool IsEdited { get; set; }
    public Guid ChatId { get; set; }
}