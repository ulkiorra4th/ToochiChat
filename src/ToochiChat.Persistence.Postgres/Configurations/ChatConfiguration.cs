using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToochiChat.Persistence.Postgres.Entities;

namespace ToochiChat.Persistence.Postgres.Configurations;

internal sealed class ChatConfiguration : IEntityTypeConfiguration<ChatEntity>
{
    public void Configure(EntityTypeBuilder<ChatEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CreationDate)
            .IsRequired();

        builder.Property(c => c.Title)
            .IsRequired();

        builder.HasMany(c => c.Members)
            .WithMany(u => u.Chats);
    }
}