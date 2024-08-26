using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToochiChat.Persistence.Postgres.Configurations;
using ToochiChat.Persistence.Postgres.Entities;

#nullable disable

namespace ToochiChat.Persistence.Postgres.Connection;

internal sealed class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<ChatEntity> Chats { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new ChatConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Postgres"));
    }
}