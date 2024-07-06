using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToochiChat.Persistence.Entities;

namespace ToochiChat.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    private const int MAX_USERNAME_LENGTH = 50;
    
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(MAX_USERNAME_LENGTH);

        builder.Property(u => u.BirthDate)
            .IsRequired();

        builder.HasOne(u => u.Account)
            .WithOne(a => a.UserInfo);

        builder.HasMany(u => u.Messages)
            .WithOne(m => m.Sender);
    }
}