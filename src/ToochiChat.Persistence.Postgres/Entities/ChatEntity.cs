using System.ComponentModel.DataAnnotations;

namespace ToochiChat.Persistence.Postgres.Entities;

public sealed class ChatEntity
{
    [Key] public Guid Id { get; set; }
    [Required] public UserEntity? Owner { get; set; }
    [Required, MaxLength(50)] public string? Title { get; set; }
    [Required] public DateTime CreationDate { get; set; }
    [Required] public ICollection<UserEntity>? Members { get; set; }
}