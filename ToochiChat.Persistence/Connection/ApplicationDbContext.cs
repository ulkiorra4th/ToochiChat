using Microsoft.EntityFrameworkCore;
using ToochiChat.Persistence.Configurations;
using ToochiChat.Persistence.Entities;

namespace ToochiChat.Persistence.Connection;

internal sealed class ApplicationDbContext : DbContext
{
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<ChatEntity> Chats { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new ChatConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}