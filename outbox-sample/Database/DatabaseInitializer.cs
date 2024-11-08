
using Microsoft.EntityFrameworkCore;

namespace outbox_sample.Database;

public class DatabaseInitializer
{
    public DatabaseInitializer(AppDbContext appDbContext)
    {
        string sqlScript = File.ReadAllText("../../Scripts/init.sql");
        appDbContext.Database.ExecuteSqlRaw(sqlScript);
    }
}