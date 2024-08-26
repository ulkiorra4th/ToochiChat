using System.ComponentModel.DataAnnotations;

namespace ToochiChat.Persistence.Postgres.Entities;

// TODO: finish entity
public sealed class MessageEntity
{
    [Key] public string? Id { get; set; }
    [Required, MaxLength(250)] public string? Content { get; set; }
    [Required] public DateTime CreationDate { get; set; }
    [Required] public string? SenderId { get; set; }
    [Required] public UserEntity? Sender { get; set; }
    [Required] public string? ChatId { get; set; }
    [Required] public ChatEntity? Chat { get; set; }
}