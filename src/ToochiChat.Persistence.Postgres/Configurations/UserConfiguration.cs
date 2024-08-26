using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToochiChat.Persistence.Postgres.Entities;

namespace ToochiChat.Persistence.Postgres.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    private const int MaxUsernameLength = 50;
    
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(MaxUsernameLength);

        builder.Property(u => u.BirthDate)
            .IsRequired();

        builder.HasOne(u => u.Account)
            .WithOne(a => a.UserInfo);

        builder.HasMany(u => u.Messages)
            .WithOne(m => m.Sender);
    }
}