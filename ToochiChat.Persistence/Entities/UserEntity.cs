using System.ComponentModel.DataAnnotations;

namespace ToochiChat.Persistence.Entities;

// TODO: finish entity
public sealed class UserEntity
{
    [Key] public string? Id { get; set; }
    [Required] public string? UserName { get; set; }
    [Required] public DateTime BirthDate { get; set; }
    [Required] public string? AccountId { get; set; }
    [Required] public AccountEntity? Account { get; set; }
    [Required] public ICollection<ChatEntity>? Chats { get; set; }
    [Required] public ICollection<MessageEntity>? Messages { get; set; }
}