using Microsoft.EntityFrameworkCore;
using outbox_sample.Database.Entities;

namespace outbox_sample.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OutboxMessage>()
            .Property(e => e.Content)
            .HasColumnType("jsonb");
    }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
}