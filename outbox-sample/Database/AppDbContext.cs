using Microsoft.EntityFrameworkCore;

namespace outbox_sample.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options);