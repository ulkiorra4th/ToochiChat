using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToochiChat.Persistence.Postgres.Entities;

// TODO: Set MaxLengths
public sealed class AccountEntity
{
    [Key] public Guid Id { get; set; }
    [EmailAddress] public string? Email { get; set; }
    [Required, MaxLength()] public string? PasswordHash { get; set; }
    [Required, MaxLength()] public string? Salt { get; set; } 
    [Required] public bool IsEmailConfirmed { get; set; }
    [Required] public DateTime CreationDate { get; set; }
    [ForeignKey(nameof(UserInfo))] public string? UserInfoId { get; set; }
    [Required] public UserEntity? UserInfo { get; set; }
}