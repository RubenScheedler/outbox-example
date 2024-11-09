using outbox_sample.Database;

namespace outbox_sample.Outbox;

public class OutboxProcessor(IServiceProvider services) : BackgroundService
{
    private Timer? _timer;
    public IServiceProvider Services { get; } = services;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        using var scope = Services.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var messagesToProcess = dbContext.OutboxMessages
            .Where(m => m.ProcessedAt == null);
        
        messagesToProcess.ToList().ForEach(message =>
        {
            // TODO transaction
            
            message.ProcessedAt = DateTimeOffset.UtcNow;
            
            // { send message to messaging system }

            // TODO set error when failed
            
            dbContext.SaveChanges();
        });
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        
        await base.StopAsync(stoppingToken);
    }
    
    public new void Dispose()
    {
        base.Dispose();
        _timer?.Dispose();
    }
}