using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToochiChat.Persistence.Entities;

namespace ToochiChat.Persistence.Configurations;

internal sealed class MessageConfiguration : IEntityTypeConfiguration<MessageEntity>
{
    private const int MAX_CONTENT_LENGTH = 250;
    
    public void Configure(EntityTypeBuilder<MessageEntity> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.CreationDate)
            .IsRequired();

        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(MAX_CONTENT_LENGTH);

        // builder.Property(m => m.Chat)
        //     .IsRequired();

        // builder.Property(m => m.Sender)
        //     .IsRequired();

        builder.HasOne(m => m.Chat)
            .WithMany(c => c.Messages);

        builder.HasOne(m => m.Sender)
            .WithMany(u => u.Messages);
    }
}