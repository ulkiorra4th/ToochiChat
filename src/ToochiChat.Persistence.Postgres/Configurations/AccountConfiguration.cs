using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToochiChat.Persistence.Postgres.Entities;

namespace ToochiChat.Persistence.Postgres.Configurations;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    private const int MaxEmailLength = 320;
    
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(MaxEmailLength);

        builder.Property(a => a.PasswordHash)
            .IsRequired()
            .HasMaxLength(86); // TODO: change

        builder.Property(a => a.Salt)
            .IsRequired()
            .HasMaxLength(86); // TODO: change

        builder.Property(a => a.CreationDate)
            .IsRequired();

        builder.Property(a => a.IsEmailConfirmed)
            .IsRequired();

        // builder.Property(a => a.UserInfo)
        //     .IsRequired();

        builder.HasOne(a => a.UserInfo)
            .WithOne(u => u.Account);
    }
}