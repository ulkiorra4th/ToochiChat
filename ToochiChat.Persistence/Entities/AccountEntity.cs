using System.ComponentModel.DataAnnotations;

namespace ToochiChat.Persistence.Entities;

// TODO: Set MaxLengths
public sealed class AccountEntity
{
    [Key] public string? Id { get; set; }
    [EmailAddress] public string? Email { get; set; }
    [Required, MaxLength()] public string? PasswordHash { get; set; }
    [Required, MaxLength()] public string? Salt { get; set; } 
    [Required] public bool IsEmailConfirmed { get; set; }
    [Required] public DateTime CreationDate { get; set; }
    [Required] public string? UserInfoId { get; set; }
    [Required] public UserEntity? UserInfo { get; set; }
    // TODO: make index
    public string? ConfirmationToken { get; set; }
}